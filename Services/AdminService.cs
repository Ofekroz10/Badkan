using MyProject.Models;
using MyProject.Models.Repositories;
using MyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepo;

        public AdminService(IAdminRepository adminRepo)
        {
            this.adminRepo = adminRepo;
        }

        public async Task<int> AddCourse(CourseViewModel courseVM)
        {
            if (adminRepo.GetCourseByName(courseVM.Course.CourseName) != null)
                throw new ArgumentException();

            Course course = courseVM.Course;
            int id  = await adminRepo.AddCourse(course);
            return id;
        }

        public void AddLecturersToCourse(IList<CourseLecturers> courseLecturers)
        {
            adminRepo.AddLecturersToCourse(courseLecturers);
        }
    }
}
