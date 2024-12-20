using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SkyGateDomainLayer.DTOs.Identity;
using SkyGateDomainLayer.Entities.Identity;
using SkyGateDomainLayer.Interfaces.Identity;
using SkyGateServiceLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateServiceLayer.Services.Identity
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly IMapper Mapper;
        private readonly IConfiguration Config;

        public AccountService(UserManager<ApplicationUser> UserManager, IMapper Mapper, IConfiguration Config)
        {
            this.UserManager = UserManager;
            this.Mapper = Mapper;
            this.Config = Config;
        }

        public async Task<List<string>> SignUpAsync(RegisterDTO UserData)
        {
            ApplicationUser User = Mapper.Map<ApplicationUser>(UserData);

            IdentityResult Result = await UserManager.CreateAsync(User, UserData.Password);

            List<string> Errors = new List<string>();

            if(!Result.Succeeded)
            {
                foreach(var Error in Result.Errors)
                {
                    Errors.Add(Error.Description);
                }
            }

            return Errors;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO UserData)
        {
            ApplicationUser User = await UserManager.FindByEmailAsync(UserData.Email);

            if(User is not null)
            {
                bool Found = await UserManager.CheckPasswordAsync(User, UserData.Password);

                if(Found)
                {
                    LoginResponseDTO Response = new LoginResponseDTO();

                    Response.Token = await IdentityHelper.GenerateJwt(User, Config, UserManager);
                    Response.ExpiteTime = DateTime.Now.AddDays(int.Parse(Config["Jwt:ExpireDays"]));

                    return Response;
                }
            }

            return null;
        }

        public async Task<UserDTO> GetCurrentUserAsync(string Email)
        {
            var User = await UserManager.FindByEmailAsync(Email);

            return Mapper.Map<UserDTO>(User);
        }
    }
}
