using Microsoft.AspNetCore.Identity;
using SkyGateDomainLayer.Classes;
using SkyGateDomainLayer.DTOs.ReservationModule;
using SkyGateDomainLayer.Entities.Identity;
using SkyGateDomainLayer.Entities.ReservationModule;
using SkyGateDomainLayer.Interfaces.Identity;
using SkyGateDomainLayer.Interfaces.ReservationModule;
using SkyGateDomainLayer.Interfaces.UnitOfWork;
using SkyGateRepositoryLayer.Repositories;
using SkyGateRepositoryLayer.Repositories.Specifications;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateServiceLayer.Services.ReservationModule
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly UserManager<ApplicationUser> UserManager;

        public ReservationService(IUnitOfWork UnitOfWork, UserManager<ApplicationUser> UserManager)
        {
            this.UnitOfWork = UnitOfWork;
            this.UserManager = UserManager;
        }

        public IAccountService AccountService { get; }

        public async Task<int> AddReservationAsync(string Email, ReservationCreateDTO Reservation)
        {
            var ReservationObject = new Reservation();

            var User = await UserManager.FindByEmailAsync(Email);

            ReservationObject.UserId = User.Id;
            ReservationObject.Flights = new List<Flight>();
            ReservationObject.Flights[0].Id = Reservation.DepartureFlightId;
            ReservationObject.Flights[1].Id = Reservation.ReturnFlightId;
            ReservationObject.NumberOfAdults = Reservation.NumberOfAdults;
            ReservationObject.NumberOfChilds = Reservation.NumberOfChilds;
            ReservationObject.CabinName = Reservation.CabinName;
            ReservationObject.SeatsToken = Reservation.SeatsToken;
            ReservationObject.PaymentIntentId = "";
            ReservationObject.ClientSecret = "";

            UnitOfWork.ReservationRepository().Add(ReservationObject);
            return UnitOfWork.SaveChanges();
        }

        public int DeleteReservation(int Id)
        {
            UnitOfWork.ReservationRepository().Delete(Id);

            return UnitOfWork.SaveChanges();
        }

        public List<ReservationResponseDTO> GetAllReservationsForSpecificUser(string UserId)
        {
            var Spec = new Specification<Reservation, int>();
            Spec.Includes.Add(P => P.User);
            Spec.Includes.Add(P => P.Flights);
            Spec.Criteria = P => P.UserId == UserId;

            var ReservationsFromDB = UnitOfWork.ReservationRepository().GetAll(Spec);

            List<ReservationResponseDTO> Reservations = new List<ReservationResponseDTO>();

            foreach(var R in ReservationsFromDB)
            {
                Reservations.Add(new ReservationResponseDTO
                {
                    UserId = R.UserId,
                    DepartureFlightId = R.Flights[0].Id,
                    ReturnFlightId = R.Flights[1].Id,
                    NumberOfAdults = R.NumberOfAdults,
                    NumberOfChilds = R.NumberOfChilds,
                    CabinName = R.CabinName,
                    SeatsToken = R.SeatsToken.ToList(),
                    PaymentIntentId = R.PaymentIntentId,
                    ClientSecret = R.ClientSecret,
                    TotalPrice = R.TotalPrice()
                });

            }

            return Reservations;
        }

        public ReservationResponseDTO GetReservationById(int Id)
        {
            var Spec = new Specification<Reservation, int>();
            Spec.Criteria = P => P.Id == Id;
            Spec.Includes.Add(P => P.User);
            Spec.Includes.Add(P => P.Flights);

            var ReservationFromDB = UnitOfWork.ReservationRepository().GetById(Spec);

            if(ReservationFromDB is null)
            {
                return null;
            }

            ReservationResponseDTO ReservationDTO = new ReservationResponseDTO();

            ReservationDTO.UserId = ReservationFromDB.UserId;
            ReservationDTO.DepartureFlightId = ReservationFromDB.Flights[0].Id;
            ReservationDTO.ReturnFlightId = ReservationFromDB.Flights[1].Id;
            ReservationDTO.NumberOfAdults = ReservationFromDB.NumberOfAdults;
            ReservationDTO.NumberOfChilds = ReservationFromDB.NumberOfChilds;
            ReservationDTO.CabinName = ReservationFromDB.CabinName;
            ReservationDTO.SeatsToken = ReservationFromDB.SeatsToken.ToList();
            ReservationDTO.PaymentIntentId = ReservationFromDB.PaymentIntentId;
            ReservationDTO.ClientSecret = ReservationFromDB.ClientSecret;
            ReservationDTO.TotalPrice = ReservationFromDB.TotalPrice();

            return ReservationDTO;
        }

        public int UpdateReservation(int Id, ReservationCreateDTO Reservation)
        {
            var ReservationObject = new Reservation();

            ReservationObject.UserId = Reservation.UserId;
            ReservationObject.Flights[0].Id = Reservation.DepartureFlightId;
            ReservationObject.Flights[1].Id = Reservation.ReturnFlightId;
            ReservationObject.NumberOfAdults = Reservation.NumberOfAdults;
            ReservationObject.NumberOfChilds = Reservation.NumberOfChilds;
            ReservationObject.CabinName = Reservation.CabinName;
            ReservationObject.SeatsToken = Reservation.SeatsToken;
            ReservationObject.PaymentIntentId = Reservation.PaymentIntentId;
            ReservationObject.ClientSecret = Reservation.PaymentIntentId;

            UnitOfWork.ReservationRepository().Update(Id, ReservationObject);
            return UnitOfWork.SaveChanges();
        }
    }
}
