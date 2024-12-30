using SkyGateDomainLayer.Entities.FlightModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.DTOs.AirplaneModule
{
    public class AirplaneDTO
    {
        public string Model { get; set; }
        [Required]
        public int NumberOfEconomySeats { get; set; }
        public int NumberOfBusinessSeats { get; set; }
        public int NumberOfFirstClassSeats { get; set; }
        public decimal AdultBaggage { get; set; }
        public decimal ChildBaggage { get; set; }
    }
}
