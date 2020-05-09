using MyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Repositories
{
    public interface ICourseLecturerRepository
    {
        public Task<int> AddCourse(Course course);
        public Task<int> AddLecturersToCourse(IList<CourseLecturers> courseLecturers);
        public Course GetCourseByName(string name);
        public IList<string> GetLecturersOfCourseAsString(Course c);
        public Task<int> EditNameToCourseAsync(Course course);
        public Course GetCourseById(int id);
        public Task<int> DeleteAllLecturerOfCourse(Course c);
        public IList<CourseLecturers> GetLecturersOfCourse(Course c);
        public IList<string> GetAllCoursesName();
        public IList<CourseLecturers> GetCourseLecturers();
        public IList<Course> GetAllCourses();
        Task<int> DeleteCourseLecturerAndCourse(Course c, IEnumerable<CourseLecturers> rows);
        public  Task<IList<Course>> GetAllCoursesOfLecturerAsync(string lecturerId);
    }
}
