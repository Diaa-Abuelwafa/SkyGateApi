using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyGateDomainLayer.DTOs.Identity;
using SkyGateDomainLayer.Errors;
using SkyGateDomainLayer.Interfaces.Identity;
using System.Net;
using System.Security.Claims;

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
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);

            var CurrentUser = await AccountService.GetCurrentUserAsync(Email);

            return Ok(CurrentUser);
        }
    }
}
