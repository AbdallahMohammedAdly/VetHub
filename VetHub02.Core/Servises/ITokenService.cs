using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities.identity;

namespace VetHub02.Core.Servises
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsnyc(AppUser user,UserManager<AppUser> userManager);
    }
}
