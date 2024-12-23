using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyGateDomainLayer.DTOs.Identity;
using SkyGateDomainLayer.Errors;
using SkyGateDomainLayer.Interfaces.Identity;
using System.Net;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SkyGateApiLayer.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService AccountService;

        public AccountController(IAccountService AccountService)
        {
            this.AccountService = AccountService;
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SignUp(RegisterDTO UserData)
        {
            List<string> Errors = await AccountService.SignUpAsync(UserData);

            if(Errors.Count() == 0)
            {
                return Created();
            }

            return BadRequest(new ApiValidationErrorResponse((int)HttpStatusCode.BadRequest, Errors));
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginResponseDTO), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login(LoginDTO UserData)
        {
            var Response = await AccountService.LoginAsync(UserData);

            if(Response is not null)
            {
                return Ok(Response);
            }

            return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest, "Invalid Email Or Password"));
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);

            var CurrentUser = await AccountService.GetCurrentUserAsync(Email);

            return Ok(CurrentUser);
        }

        [HttpPost("SendEmail")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EmailToResetPassword([FromQuery] string EmailAddress)
        {
            bool Found = await AccountService.SendEmailToResetPasswordAsync(EmailAddress);

            if(!Found)
            {
                return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest, "No User With This Email Address"));
            }

            return Created();
        }

        [HttpPost("Reset")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse),(int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO ResetPasswordData)
        {
            List<string> Result = await AccountService.ResetPasswordAsync(ResetPasswordData.Email, ResetPasswordData.Token, ResetPasswordData.NewPassword);

            if(Result.Count() == 0)
            {
                return Created();
            }

            return BadRequest(new ApiValidationErrorResponse((int)HttpStatusCode.BadRequest, Result));
        }
    }
}
