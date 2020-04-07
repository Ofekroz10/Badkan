using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Dtos
{
    public class UserDto
    {
        [Key]
        [Required]
        //[Length(UserIdentityConfig.ID_LENGTH)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
