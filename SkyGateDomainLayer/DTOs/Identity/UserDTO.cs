using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.DTOs.Identity
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HomeAddress { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportNumber { get; set; }

        // ToDo : IQueryable<Reservation>
        // The Relationship With The Reservation Model
    }
}
