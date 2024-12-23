using SkyGateDomainLayer.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Interfaces.Identity
{
    public interface IAccountService
    {
        public Task<List<string>> SignUpAsync(RegisterDTO UserData);
        public Task<LoginResponseDTO> LoginAsync(LoginDTO UserData);
        public Task<UserDTO> GetCurrentUserAsync(string Email);

        public Task<bool> SendEmailToResetPasswordAsync(string Email);
        public Task<List<string>> ResetPasswordAsync(string Email, string Token, string NewPassword);
    }
}
