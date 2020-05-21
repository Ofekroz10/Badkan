using Microsoft.AspNetCore.Mvc.Rendering;
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

        public ExerciseCourseVM GetAllMissionsOfCourse(int courseId, string courseName)
        {
            var exercises = lecturerRepo.GetAllMissionsOfCourse(courseId);
            ExerciseCourseVM vm = new ExerciseCourseVM()
            {
                CourseName = courseName,
                Lst = new List<SelectListItem>(),
                Choosen = new Exercise() { Id = 1, Title = "a", Description = "a", GitHubLink = "a" }

            };

            foreach (var x in exercises)
                vm.Lst.Add(new SelectListItem { Value = x.ExId.ToString(),
                    Text = lecturerRepo.GetExerciseById(x.ExId).Title });

            return vm;
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
