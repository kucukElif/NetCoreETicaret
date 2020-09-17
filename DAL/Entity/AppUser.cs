using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entity
{
    //Microsoft.AspNetCore.Identity.EntityFrameworkCore kütüphanesine ihtiyacınız var.
    public class AppUser:IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public bool Gender { get; set; }

    }
}
