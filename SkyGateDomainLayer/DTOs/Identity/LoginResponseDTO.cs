using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.DTOs.Identity
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public DateTime ExpiteTime { get; set; }
    }
}
