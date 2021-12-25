using Accounting.Shared.ViewModels.PersonViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels.LegalPersonViewModels
{
    public class LegalPersonCreationDTO
    {
        [Required(ErrorMessage = "Legalperson address is a required field.")]
        [MaxLength(200, ErrorMessage = "Maximum length for the address is 30 characters.")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Legalperson phone number is a required field.")]
        [MaxLength(11, ErrorMessage = "Maximum length for the phone number is 30 characters.")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Legalperson email is a required field.")]
        [MaxLength(200, ErrorMessage = "Maximum length for the email is 30 characters.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Legalperson company No is a required field.")]
        [MaxLength(4, ErrorMessage = "Maximum length for the company No is 30 characters.")]
        public string CompanyNo { get; set; }


        [Required(ErrorMessage = "Legalperson registration code is a required field.")]
        [MaxLength(4, ErrorMessage = "Maximum length for the registration code is 30 characters.")]
        public string RegistrationCode { get; set; }


        [Required(ErrorMessage = "Legalperson economic code is a required field.")]
        [MaxLength(4, ErrorMessage = "Maximum length for the economic code is 30 characters.")]
        public string EconomicCode { get; set; }
    }
}
