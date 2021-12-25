using Accounting.Shared.ViewModels.RealPersonViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels.CashViewModels
{
    public class CashCreationDTO
    {
        [Required(ErrorMessage = "Cash name is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the cash name is 20 characters.")]
        public string CashName { get; set; }

        public int RealPersonId { get; set; }
    }
}
