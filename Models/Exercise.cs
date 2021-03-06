﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [RegularExpression(Validations.LecturerValidationErorr.GithubRegex)]
        public string GitHubLink { get; set; }
    }
}
