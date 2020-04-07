using MyProject.Models;
using MyProject.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.ViewModel
{
    public class RegisterViewModel
    {

        [Key]
        [Required]
        [Length(UserIdentityConfig.ID_LENGTH)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm password")]
        [Compare("Password",
            ErrorMessage ="Password and confirm password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
