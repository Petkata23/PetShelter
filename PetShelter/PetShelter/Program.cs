using EntityFrameworkCore.UseRowNumberForPaging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetShelter;
using PetShelter.Data;
using PetShelter.Data.Repos;
using PetShelter.Services.Services;
using PetShelter.Shared.Extensions;
using PetShelter.Shared.Security;
using PetShelter.Shared.Security.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(m => m.AddProfile(new AutoMapperConfiguration()));
builder.Services.AutoBind(typeof(PetsService).Assembly);
builder.Services.AutoBind(typeof(PetRepository).Assembly);
builder.Services.AddAutoMapper(m => m.AddProfile((new AutoMapperConfiguration())));

IJwtSettings settings = builder.Configuration.GetSection(typeof(JwtSettings).Name).Get<JwtSettings>();

builder.Services.AddAuthentication(cfg => cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    if (options.SecurityTokenValidators.FirstOrDefault() is JwtSecurityTokenHandler jwtSecurityTokenHandler)
                    {
                        jwtSecurityTokenHandler.MapInboundClaims = false;
                    }
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = settings.Issuer,
                        ValidAudience = settings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key)),
                        NameClaimType = JwtRegisteredClaimNames.Sub
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/chathub")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

builder.Services.AddSingleton(settings);

builder.Services.AddDbContext<PetShelterDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"], i => i.UseRowNumberForPaging());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PetShelterDbContext>();
    // Automatically update database
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
