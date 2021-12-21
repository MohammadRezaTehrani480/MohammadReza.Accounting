﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.ViewModels.LegalPersonViewModels
{
    public class LegalPersonCreationDTO
    {
        [Required(ErrorMessage = "Legalperson company No is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the company No is 30 characters.")]
        public int CompanyNo { get; set; }


        [Required(ErrorMessage = "Legalperson registration code is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the registration code is 30 characters.")]
        public int RegistrationCode { get; set; }


        [Required(ErrorMessage = "Legalperson economic code is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the economic code is 30 characters.")]
        public int EconomicCode { get; set; }
    }
}
