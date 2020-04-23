using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Display(Name ="Course name")]
        public string CourseName { get; set; }

    }
}
