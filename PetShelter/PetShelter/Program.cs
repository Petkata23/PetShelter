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
using Microsoft.AspNetCore.Authentication.Cookies;
using PetShelter.Shared.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(m => m.AddProfile(new AutoMapperConfiguration()));
builder.Services.AutoBind(typeof(PetsService).Assembly);
builder.Services.AutoBind(typeof(PetRepository).Assembly);
builder.Services.AddAutoMapper(m => m.AddProfile(new AutoMapperConfiguration()));
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ISheltersService, SheltersService>();
builder.Services.AddScoped<IVaccinesService, VaccinesService>();
builder.Services.AddScoped<IPetVaccinesService, PetVaccinesService>();
builder.Services.AddScoped<IPetsService, PetsService>();

IJwtSettings settings = builder.Configuration.GetSection(typeof(JwtSettings).Name).Get<JwtSettings>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

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
