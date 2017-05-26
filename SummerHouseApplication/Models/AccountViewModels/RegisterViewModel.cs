using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SummerHouseApplication.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Sähköposti")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Salasanan tulee olla vähintään kuusi merkkiä pitkä.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Salasana")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Vahvista salasana")]
        [Compare("Password", ErrorMessage = "Salasanat eivät täsmää.")]
        public string ConfirmPassword { get; set; }
    }
}
