using Accounting.Shared.ViewModels.DocumentsViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels.PersonViewModels
{
    public class PersonDTO
    {
        [Required(ErrorMessage = "Legalperson address is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the address is 30 characters.")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Legalperson phone number is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the phone number is 30 characters.")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Legalperson email is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the email is 30 characters.")]
        public string Email { get; set; }


        public IList<DocumentDTO> Documents { get; set; }
    }
}
