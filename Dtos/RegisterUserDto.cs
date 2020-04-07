
using MyProject.Validations;
using MyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Length(UserIdentityConfig.ID_LENGTH)]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
