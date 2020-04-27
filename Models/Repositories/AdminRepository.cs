using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        MyDbContext dbContext;

        public AdminRepository(MyDbContext db)
        {
            this.dbContext = db;
        }

        public async Task<int> AddCourse(Course course)
        {
            dbContext.Courses.Add(course);
            await dbContext.SaveChangesAsync();
            return course.CourseId;
        }

        public async void AddLecturersToCourse(IList<CourseLecturers> courseLecturers)
        {
            foreach(var x in courseLecturers)
            {
                dbContext.CourseLecturers.Add(new CourseLecturers{
                    CourseId = x.CourseId,
                    LecturerId = x.LecturerId
                }) ;
            }

            await dbContext.SaveChangesAsync();
        }

        public Course GetCourseByName(string name)
        {
            return dbContext.Courses.SingleOrDefault(x => x.CourseName == name);
        }
    }
}
