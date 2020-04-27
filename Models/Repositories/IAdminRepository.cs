using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Repositories
{
    public interface IAdminRepository
    {
        public Task<int> AddCourse(Course course);
        public void AddLecturersToCourse(IList<CourseLecturers> courseLecturers);
        public Course GetCourseByName(string name);
    }
}
