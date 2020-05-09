using MyProject.Models;
using MyProject.Models.Reposetories;
using MyProject.Models.Repositories;
using MyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class LecturerService : ILecturerService
    {
        private readonly ICourseLecturerRepository lecturerRepo;

        public LecturerService(ICourseLecturerRepository lecturerRepo)
        {
            this.lecturerRepo = lecturerRepo;
        }


        public async Task<IEnumerable<CourseViewModel>> GetAllMyCoursesAsync(string lecturerId)
        {
            var courses = await lecturerRepo.GetAllCoursesOfLecturerAsync(lecturerId);
            IList<CourseViewModel> vm = new List<CourseViewModel>();

            foreach (var c in courses)
                vm.Add(CourseViewModel.CreateVMfromLecturersCourse(c, lecturerRepo.GetLecturersOfCourseAsString(c)));
            return vm;
        }
    }
}
