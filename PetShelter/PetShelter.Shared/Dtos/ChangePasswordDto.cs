using System;
using System.Collections.Generic;
using System.Text;

namespace PetShelter.Shared.Dtos
{
    public class ChangePasswordDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string NewPassword { get; set; }
    }
}
