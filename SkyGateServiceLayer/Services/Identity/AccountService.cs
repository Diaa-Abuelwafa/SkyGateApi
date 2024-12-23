using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SkyGateDomainLayer.Classes;
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
            // Check For Uniqueness Email

            var CheckEmail = await UserManager.FindByEmailAsync(UserData.Email);

            List<string> Errors = new List<string>();

            if (CheckEmail is not null)
            {
                Errors.Add("This Email Address Already Exsists");

                return Errors;
            }

            ApplicationUser User = Mapper.Map<ApplicationUser>(UserData);

            IdentityResult Result = await UserManager.CreateAsync(User, UserData.Password);

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

        public async Task<bool> SendEmailToResetPasswordAsync(string EmailAddress)
        {
            var UserExsist = await UserManager.FindByEmailAsync(EmailAddress);

            if(UserExsist is null)
            {
                return false;
            }

            Email Mail = new Email();

            Mail.To = EmailAddress;
            Mail.Subject = "Reset Password";

            var Token = await UserManager.GeneratePasswordResetTokenAsync(UserExsist);

            // TODO : When I Make A FrontEnd Then Complete The Url That Include The Reset Password Url Page
            Mail.Body = Token;

            EmailHelper.SendEmail(Mail, Config);

            return true;
        }

        public async Task<List<string>> ResetPasswordAsync(string EmailAddress, string Token, string NewPassword)
        {
            var User = await UserManager.FindByEmailAsync(EmailAddress);

            List<string> Errors = new List<string>();

            if(User is null)
            {
                Errors.Add("No User With This Email Address");

                return Errors;
            }

            IdentityResult Result = await UserManager.ResetPasswordAsync(User, Token, NewPassword);

            if(!Result.Succeeded)
            {
                foreach(var Error in Result.Errors)
                {
                    Errors.Add(Error.Description);
                }
            }

            return Errors;
        }
    }
}
