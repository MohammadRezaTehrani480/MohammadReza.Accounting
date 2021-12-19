using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels.CashViewModels
{
    public abstract class CashForManipulationDto
    {
        [Required(ErrorMessage = "Cash name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the cash name is 20 characters.")]
        public string CashName { get; set; }
    }
}
