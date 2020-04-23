using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class CourseLecturers
    {
        public int CourseId { get; set; }
        public int LecturerId { get; set; }
    }
}
