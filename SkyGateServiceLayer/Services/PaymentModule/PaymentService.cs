using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SkyGateDomainLayer.Entities.ReservationModule;
using SkyGateDomainLayer.Enums.PaymentModule;
using SkyGateDomainLayer.Interfaces.PaymentModule;
using SkyGateDomainLayer.Interfaces.ReservationModule;
using SkyGateDomainLayer.Interfaces.UnitOfWork;
using SkyGateRepositoryLayer.Repositories.Specifications;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateServiceLayer.Services.PaymentModule
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration Config;
        private readonly IUnitOfWork UnitOfWork;

        public PaymentService(IConfiguration Config, IUnitOfWork UnitOfWork)
        {
            this.Config = Config;
            this.UnitOfWork = UnitOfWork;
        }

        public void ChangeStatus(int ReservationId, bool Flag)
        {
            var Spec = new Specification<Reservation, int>();
            Spec.Criteria = P => P.Id == ReservationId;

            var Reservation = UnitOfWork.ReservationRepository().GetById(Spec);

            if (Reservation is null)
            {
                return;
            }

            if(Flag)
            {
                Reservation.Status = PaymentStatus.Received;
            }
            else
            {
                Reservation.Status = PaymentStatus.Failed;
            }
        }

        public async Task<PaymentIntent> CreateOrUpdatePaymentIntentAsync(int ReservationId)
        {
            // Authorize To Stripe
            StripeConfiguration.ApiKey = Config["Stripe:SecretKey"];

            // Get The Reservation
            var Spec = new Specification<Reservation, int>();
            Spec.Criteria = P => P.Id == ReservationId;

            var Reservation = UnitOfWork.ReservationRepository().GetById(Spec);

            if(Reservation is null)
            {
                return null;
            }

            if(string.IsNullOrEmpty(Reservation.PaymentIntentId))
            {
                // Create New PaymentIntentId

                // Make CreateOptions
                PaymentIntentCreateOptions Options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)Reservation.TotalPrice() * 100,
                    PaymentMethodTypes = {"card"},
                    Currency = "usd"
                };

                // Crete PaymentIntentId
                var Service = new PaymentIntentService();

                var PaymentIntent = await Service.CreateAsync(Options);

                Reservation.PaymentIntentId = PaymentIntent.Id;
                Reservation.ClientSecret = PaymentIntent.ClientSecret;

                UnitOfWork.ReservationRepository().Update(ReservationId, Reservation);
                UnitOfWork.SaveChanges();

                return PaymentIntent;
            }
            else
            {
                // Update New PaymentIntentId

                // Make UpdateOptions
                PaymentIntentUpdateOptions Options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)Reservation.TotalPrice() * 100,
                };

                // Crete PaymentIntentId
                var Service = new PaymentIntentService();

                var PaymentIntent = await Service.UpdateAsync(Reservation.PaymentIntentId, Options);

                Reservation.PaymentIntentId = PaymentIntent.Id;
                Reservation.ClientSecret = PaymentIntent.ClientSecret;

                UnitOfWork.ReservationRepository().Update(ReservationId, Reservation);
                UnitOfWork.SaveChanges();

                return PaymentIntent;
            }
        }
    }
}
