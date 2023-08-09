using System;
using Microsoft.AspNetCore.Identity;

namespace ECPMaster.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String Name { get; set; }
    }
}
