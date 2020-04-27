using MyProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public IList<CourseLecturers> GetAllSelectedLecturers()
        {
            IList<CourseLecturers> lst = new List<CourseLecturers>();
            foreach( var x in List)
            {
                if (x.IsChecked)
                {
                    lst.Add(new CourseLecturers
                    {
                        LecturerId = int.Parse(x.LecturerId),
                        CourseId = Course.CourseId
                    }) ;
                }
            }

            return lst;
                
        }
    }

    public class IsCheckedLecturer
    {
        public bool IsChecked { get; set; }
        public string LecturerId { get; set; }
    }
}
