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
        public Task AddLecturersToCourseAsync(IList<CourseLecturers> courseLecturers);
        public CourseViewModel GetCourseViewModel(string courseName);
        public Task<int> EditCourseAsync(CourseViewModel courseVM);
        public IEnumerable<CourseViewModel> GetUserLecturerWithCourseName();
        public Task<int> DeleteCourse(string courseName);
        public Exercise GetExerciseById(int exId);
    }
}
