using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyGateDomainLayer.DTOs.FlightModule;
using SkyGateDomainLayer.Errors;
using SkyGateDomainLayer.Interfaces.FlightModule;
using System.Net;

namespace SkyGateApiLayer.Controllers.FlightModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService FlightService;

        public FlightController(IFlightService FlightService)
        {
            this.FlightService = FlightService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<FlightDTO>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllFlights()
        {
            var Flights = FlightService.GetAllFlights();

            return Ok(Flights);
        }

        [HttpGet("Special")]
        [ProducesResponseType(typeof(List<FlightDTO>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllFlightsWithSpec(FlightParams Params)
        {
            var Flights = FlightService.GetAllFlightsWithSpec(Params);

            return Ok(Flights);
        }

        [HttpGet("{Id:int}")]
        [ProducesResponseType(typeof(FlightDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.NotFound)]
        public IActionResult GetFlightById(int Id)
        {
            var Flight = FlightService.GetFlightById(Id);

            if(Flight is not null)
            {
                return Ok(Flight);
            }

            return NotFound(new ApiErrorResponse((int)HttpStatusCode.NotFound, "No Flight With This Id"));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddFlight(FlightCreateDTO Flight)
        {
            var Result = FlightService.AddFlight(Flight);

            if(Result > 0)
            {
                return Created();
            }

            return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest));
        }

        [HttpPut("{Id:int}")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateFlight(int Id, FlightCreateDTO Flight)
        {
            var Result = FlightService.UpdateFlight(Id, Flight);

            if (Result > 0)
            {
                return Created();
            }

            return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest));
        }

        [HttpDelete("{Id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.NotFound)]
        public IActionResult DeleteFlight(int Id)
        {
            var Result = FlightService.DeleteFlight(Id);

            if(Result > 0)
            {
                return NoContent();
            }

            return NotFound(new ApiErrorResponse((int)HttpStatusCode.NotFound, "No Flight With This Id"));
        }
    }
}
