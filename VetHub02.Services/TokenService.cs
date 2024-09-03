using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities.identity;
using VetHub02.Core.Servises;
using System.Security.Cryptography;

namespace VetHub02.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateTokenAsnyc(AppUser user, UserManager<AppUser> userManager)
        {
            var authClaims = new List<Claim>()  
            {
                new Claim(ClaimTypes.GivenName,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email),
            };

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));



            var token = new JwtSecurityToken(

                issuer: configuration["JWT:VaildIssuer"],
                audience: configuration["JWT:VaildAudience"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
                claims:authClaims,
                signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)

                ) ;
            
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
       
    }
}
