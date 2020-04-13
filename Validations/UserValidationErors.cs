using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Validations
{
    public static class UserValidationErors
    {
        public const string EMAIL_EXIST_EROR_MSG = "Email '{0}' address already exist";
        public const string LOGIN_FAILED_EROR_MSG = "Login failed";
        public const string USER_DO_NOT_EXIST = "User '{0}' do not exist";
        public const string USER_ALREADY_EXIST = "User '{0}' already exist";
        public const string INCORRECT_PASSWORD = "Old password is not correct";
        public const string ID_LENGTH = "The length is not {0}";

    }
}
