﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Validations
{
    public static class LecturerValidationErorr
    {
        public const string UserIsNotLecturer = "User {0} is not lecturer!";
        public const string GithubRegex = @"^(?:http(?:s)?:\/\/)?(?:[^\.]+\.)?github\.com";
    }
}
