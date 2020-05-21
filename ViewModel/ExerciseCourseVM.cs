using Microsoft.AspNetCore.Mvc.Rendering;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.ViewModel
{
    public class ExerciseCourseVM
    {
        public string CourseName { get; set; }
        public IList<SelectListItem> Lst { get; set; }
        public Exercise Choosen { get; set; }
    }
}
