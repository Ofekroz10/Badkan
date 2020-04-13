using Microsoft.AspNetCore.Mvc;
using MyProject.Controllers;
using MyProject.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.ViewModel
{
    public class UpdateUserViewModel
    {
        [Key]
        [Required]
        [Length(UserIdentityConfig.ID_LENGTH)]
        public long Id { get; set; }


        public long PreId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        [Remote(action: "ChackOldPassword", controller: "Account", HttpMethod = "POST",
            AdditionalFields = nameof(OldPassword)+","+nameof(NewPassword)+","+nameof(Id))]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


    }
}
