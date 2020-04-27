using MyProject.Models;
using MyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public interface IAdminService
    {
        public Task<int> AddCourse(CourseViewModel course);
        public void AddLecturersToCourse(IList<CourseLecturers> courseLecturers);
    }
}
