using MyProject.Models.Reposetories;
using MyProject.Services;
using MyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Repositories
{
    public class CourseLecturerRepository : ICourseLecturerRepository
    {
        MyDbContext dbContext;
        IUserRepository userRepo;


        public CourseLecturerRepository(MyDbContext db, IUserRepository userRepo)
        {
            this.dbContext = db;
            this.userRepo = userRepo;
        }

        public async Task<int> AddCourse(Course course)
        {
            dbContext.Courses.Add(course);
            int x = await dbContext.SaveChangesAsync();
            if (x <= 0)
                throw new Exception("Gabish");
            return course.CourseId;
        }

        public async Task<int> AddLecturersToCourse(IList<CourseLecturers> courseLecturers)
        {
            foreach(var x in courseLecturers)
            {
                dbContext.CourseLecturers.Add(new CourseLecturers{
                    CourseId = x.CourseId,
                    LecturerId = x.LecturerId
                }) ;
            }

            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAllLecturerOfCourse(Course c)
        {
            var courseLecturersLst = GetLecturersOfCourse(c);
            foreach (var row in courseLecturersLst)
                dbContext.CourseLecturers.Remove(row);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteCourseLecturerAndCourse(Course c, IEnumerable<CourseLecturers> rows)
        {
            foreach (var row in rows)
                dbContext.CourseLecturers.Remove(row);
            dbContext.Courses.Remove(c);

            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> EditNameToCourseAsync(Course course)
        {
            var courseFromDb = GetCourseById(course.CourseId);
            courseFromDb.CourseName = course.CourseName;
            return await dbContext.SaveChangesAsync();
        }

        public IList<Course> GetAllCourses()
        {
            return dbContext.Courses.ToList();
        }

        public IList<string> GetAllCoursesName()
        {
            return dbContext.Courses.Select(x => x.CourseName).ToList();
        }

        public async Task<IList<Course>> GetAllCoursesOfLecturerAsync(string lecturerId)
        {
            bool isAdmin = await userRepo.
               IsUserBelongToRole(lecturerId, UserRollsType.Admin.IntValueAsString());

            IList<Course> courses = new List<Course>();

            if (isAdmin)
                courses = GetAllCourses().ToList();
            else
            {
                var coursesId = dbContext.CourseLecturers.Where(x => x.LecturerId == long.Parse(lecturerId))
                          .Select(x => x.CourseId).ToList();

                foreach (var x in coursesId)
                    courses.Add(GetCourseById(x));
            }
            
            return courses;

        }

        public IList<ExerciseCourses> GetAllMissionsOfCourse(int courseName)
        {
            return (dbContext.ExercisesCourses.Where(x => x.CourseId == courseName).ToList());
        }

        public Course GetCourseById(int id)
        {
            return dbContext.Courses.FirstOrDefault(x => x.CourseId == id);
        }

        public Course GetCourseByName(string name)
        {
            return dbContext.Courses.SingleOrDefault(x => x.CourseName == name);
        }

        public IList<CourseLecturers> GetCourseLecturers()
        {
            return dbContext.CourseLecturers.ToList();
        }

        public Exercise GetExerciseById(int exId)
        {
            return dbContext.Exercises.SingleOrDefault(x => x.Id == exId);
        }

        public IList<CourseLecturers> GetLecturersOfCourse(Course c)
        {
            return dbContext.CourseLecturers.Where(row => row.CourseId == c.CourseId).ToList();
        }

        public IList<string> GetLecturersOfCourseAsString(Course c)
        {
            return dbContext.CourseLecturers.Where(row => row.CourseId == c.CourseId)
                .Select(row => row.LecturerId.ToString()).ToList();
        }


    }
}
