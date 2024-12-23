using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SkyGateDomainLayer.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateServiceLayer.Helpers
{
    public static class IdentityHelper
    {
        public async static Task<string> GenerateJwt(ApplicationUser User, IConfiguration Config, UserManager<ApplicationUser> UserManager)
        {

            List<Claim> Claims = new List<Claim>();

            Claims.Add(new Claim(ClaimTypes.NameIdentifier, User.Id));
            Claims.Add(new Claim(ClaimTypes.Email, User.Email));
            Claims.Add(new Claim("Jti", Guid.NewGuid().ToString()));
            var Roles = await UserManager.GetRolesAsync(User);
            foreach(var Role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, Role));
            }

            SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:SecretKey"]));
            SigningCredentials Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var DesignOfJwt = new JwtSecurityToken
                (
                    issuer: Config["Jwt:ProviderUrl"],
                    audience : Config["Jwt:ConsumerUrl"],
                    expires: DateTime.Now.AddDays(int.Parse(Config["Jwt:ExpireDays"])),
                    claims: Claims,
                    signingCredentials: Credentials
                );

            var Token = new JwtSecurityTokenHandler().WriteToken(DesignOfJwt);

            return Token;
        }
    }
}
