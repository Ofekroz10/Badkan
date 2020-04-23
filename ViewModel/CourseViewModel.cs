using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.ViewModel
{
    public class CourseViewModel
    {
        public Course Course { get; set; }
        public IList<IsCheckedLecturer> List { get; set; }

        public CourseViewModel()
        {
            List = new List<IsCheckedLecturer>();
        }
    }

    public class IsCheckedLecturer
    {
        public bool IsChecked { get; set; }
        public string LecturerId { get; set; }
    }
}
