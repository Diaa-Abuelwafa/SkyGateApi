using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyGateDomainLayer.DTOs.AirplaneModule;
using SkyGateDomainLayer.Entities.AirplaneModule;
using SkyGateDomainLayer.Errors;
using SkyGateDomainLayer.Interfaces.AirplaneModule;
using System.Net;

namespace SkyGateApiLayer.Controllers.AirplaneModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirplaneController : ControllerBase
    {
        private readonly IAirplaneService AirplaneService;
        public AirplaneController(IAirplaneService AirplaneService)
        {
            this.AirplaneService = AirplaneService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AirplaneDTO>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllAirplanes()
        {
            var Airplanes = AirplaneService.GetAllAirplanes();

            return Ok(Airplanes);
        }

        [HttpGet]
        [Route("Free")]
        [ProducesResponseType(typeof(List<AirplaneDTO>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllFreeAirplanes()
        {
            var Airplanes = AirplaneService.GetAllFreeAirplanes();

            return Ok(Airplanes);
        }

        [HttpGet]
        [Route("{Id:int}")]
        [ProducesResponseType(typeof(AirplaneDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.NotFound)]
        public IActionResult GetAirplaneById(int Id)
        {
            var Airplane = AirplaneService.GetAirplaneById(Id);

            if (Airplane is not null)
            {
                return Ok(Airplane);
            }

            return NotFound(new ApiErrorResponse((int)HttpStatusCode.BadRequest, "No Airplane With This Id"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(AirplaneDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddAirplane(AirplaneDTO Airplane)
        {
            int Result = AirplaneService.AddAirplane(Airplane);

            if (Result > 0)
            {
                return Created();
            }

            return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest));
        }

        [HttpPut("{Id:int}")]
        [ProducesResponseType(typeof(AirplaneDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateAirplane(int Id, AirplaneDTO Airplane)
        {
            int Result = AirplaneService.UpdateAirplane(Id, Airplane);

            if (Result > 0)
            {
                return Created();
            }

            return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest));
        }

        [HttpDelete("{Id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteAirplane(int Id)
        {
            int Result = AirplaneService.DeleteAirplane(Id);

            if(Result > 0)
            {
                return NoContent();
            }

            return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest, "No Airplane With This Id")); ;
        }
    }
}
