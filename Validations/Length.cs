using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Validations
{
    public class Length : ValidationAttribute
    {
        int len = 0;
        static string erorMsg;
        public Length(int len)
        {
            this.len = len;
            erorMsg = string.Format(UserValidationErors.ID_LENGTH,len);
        }

        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            string asStr = value.ToString();
            bool isValid = asStr.Length == len ? true : false;

            if (isValid)
                return ValidationResult.Success;

            return new ValidationResult(erorMsg);
        }
    }
}
