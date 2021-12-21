using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels
{
    public class LookupCreationDTO
    {
        [Required(ErrorMessage = "Lookup title is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the titlt is 20 characters.")]
        public string Title { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than Zero")]
        public int LookupTypeId { get; set; }
    }
}
