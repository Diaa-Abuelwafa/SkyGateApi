using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyGateDomainLayer.DTOs.PatmentModule;
using SkyGateDomainLayer.Interfaces.PaymentModule;
using Stripe;

namespace SkyGateApiLayer.Controllers.PaymentModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService PaymentService;

        public PaymentController(IPaymentService PaymentService)
        {
            this.PaymentService = PaymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent(int ReservationId)
        {
            var PaymentIntent = await PaymentService.CreateOrUpdatePaymentIntentAsync(ReservationId);

            PaymentResponse Response = new PaymentResponse()
            {
                PaymentIntentId = PaymentIntent.Id,
                ClientSecret = PaymentIntent.ClientSecret
            };

            return Ok(Response);
        }

        [HttpPost("webhock")]
        public IActionResult WebhockEndPoint()
        {
            PaymentService.ChangeStatus(2, true);

            return Created();
        }
    }
}
