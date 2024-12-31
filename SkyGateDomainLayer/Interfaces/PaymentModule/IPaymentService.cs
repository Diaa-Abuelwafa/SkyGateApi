using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Interfaces.PaymentModule
{
    public interface IPaymentService
    {
        public Task<PaymentIntent> CreateOrUpdatePaymentIntentAsync(int ReservationId);

        public void ChangeStatus(int ReservationId, bool Flag);
    }
}
