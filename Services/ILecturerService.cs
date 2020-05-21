using MyProject.Models;
using MyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public interface ILecturerService
    {
        public Task<IEnumerable<CourseViewModel>> GetAllMyCoursesAsync(string lecturerId);
        public ExerciseCourseVM GetAllMissionsOfCourse(int courseId,string courseName);
    }
}
