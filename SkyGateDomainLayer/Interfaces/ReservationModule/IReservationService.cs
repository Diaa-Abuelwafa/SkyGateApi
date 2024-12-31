using SkyGateDomainLayer.DTOs.ReservationModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Interfaces.ReservationModule
{
    public interface IReservationService
    {
        public List<ReservationResponseDTO> GetAllReservationsForSpecificUser(string UserId);
        public ReservationResponseDTO GetReservationById(int Id);
        public Task<int> AddReservationAsync(string Email, ReservationCreateDTO Reservation);
        public int UpdateReservation(int Id, ReservationCreateDTO Reservation);
        public int DeleteReservation(int Id);
    }
}
