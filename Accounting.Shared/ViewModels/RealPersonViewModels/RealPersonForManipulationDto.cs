using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels.RealPersonViewModels
{
    public abstract class RealPersonForManipulationDto
    {
        [Required(ErrorMessage = "Realperson address is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the address is 30 characters.")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Realperson Phone number is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the phone number is 30 characters.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Realperson Email is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Email is 30 characters.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Realperson national code is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the national code is 10 characters.")]
        public string NationalCode { get; set; }

        [Required(ErrorMessage = "Realperson first name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the firstname is 30 characters.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Realperson last name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the last name is 30 characters.")]
        public string LastName { get; set; }

        public virtual LookupDTO BirthPlace { get; set; }

        public DateTime BirthDate { get; set; }

        public string FatherName { get; set; }

        public virtual LookupDTO Nationality { get; set; }


        [Range(10, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 10")]
        public int Age { get; set; }
    }
}
