using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyGateDomainLayer.DTOs.ReservationModule;
using SkyGateDomainLayer.Entities.ReservationModule;
using SkyGateDomainLayer.Errors;
using SkyGateDomainLayer.Interfaces.ReservationModule;
using System.Net;
using System.Security.Claims;

namespace SkyGateApiLayer.Controllers.ReservationModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService ReservationService;

        public ReservationController(IReservationService ReservationService)
        {
            this.ReservationService = ReservationService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ReservationResponseDTO>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllReservationForCurrentUser()
        {
            var Id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var Reservations = ReservationService.GetAllReservationsForSpecificUser(Id);

            return Ok(Reservations);
        }

        [HttpGet("{Id:int}")]
        [ProducesResponseType(typeof(ReservationResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.NotFound)]
        public IActionResult GetReservationById(int Id)
        {
            var Reservation = ReservationService.GetReservationById(Id);

            if(Reservation is not null)
            {
                return Ok(Reservation);
            }

            return NotFound(new ApiErrorResponse((int)HttpStatusCode.NotFound, "No Reservation With This Id"));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddReservation(ReservationCreateDTO Reservation)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);

            if(Email is null)
            {
                return BadRequest(new ApiValidationErrorResponse((int)HttpStatusCode.BadRequest));
            }

            int Result = await ReservationService.AddReservationAsync(Email, Reservation);

            if(Result > 0)
            {
                return Created();
            }

            return BadRequest(new ApiValidationErrorResponse((int)HttpStatusCode.BadRequest));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateReservation(ReservationCreateDTO Reservation)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);

            if (Email is null)
            {
                return BadRequest(new ApiValidationErrorResponse((int)HttpStatusCode.BadRequest));
            }

            int Result = await ReservationService.AddReservationAsync(Email, Reservation);

            if (Result > 0)
            {
                return Created();
            }

            return BadRequest(new ApiValidationErrorResponse((int)HttpStatusCode.BadRequest));
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public IActionResult DeleteReservation(int Id)
        {
            var Result = ReservationService.DeleteReservation(Id);

            if(Result > 0)
            {
                return NoContent();
            }

            return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest));
        }
    }
}
