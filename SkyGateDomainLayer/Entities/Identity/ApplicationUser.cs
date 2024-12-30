using Microsoft.AspNetCore.Identity;
using SkyGateDomainLayer.Entities.ReservationModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Entities.Identity
{
    public class ApplicationUser : IdentityUser 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomeAddress { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportNumber { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
