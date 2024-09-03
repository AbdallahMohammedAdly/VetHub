using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace VetHub02.Core.Entities.identity
{
    public class AppUser : IdentityUser 
    {
        public string DisplayName { get; set; } = string.Empty;   
        
        public string? Code {  get; set; }
    }
}
