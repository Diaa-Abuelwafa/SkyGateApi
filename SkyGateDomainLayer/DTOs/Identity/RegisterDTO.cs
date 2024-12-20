using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.DTOs.Identity
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "First Name Is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName Name Is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmed Password Name Is Required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmedPassword { get; set; }

        [Required(ErrorMessage = "Email Address Is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Passport Number Is Required")]
        public string PassportNumber { get; set; }

        [Required(ErrorMessage = "Home Address Is Required")]
        public string HomeAddress { get; set; }

        [Required(ErrorMessage = "Country Code Name Is Required")]
        [RegularExpression("^\\+\\d{1,4}$")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "Phone Number Is Required")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

    }
}
