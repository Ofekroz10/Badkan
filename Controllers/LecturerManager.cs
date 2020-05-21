using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Services;
using MyProject.ViewModel;

namespace MyProject.Controllers
{
    public class LecturerManager : Controller
    {
        private readonly ILecturerService lecturerService;

        public LecturerManager(ILecturerService lecturerService)
        {
            this.lecturerService = lecturerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetManageMyCoure(long id)
        {
            if (UserRollsTypeExtentions.AdminOrLecturer(User))
                return View("CourseManagment", await lecturerService.GetAllMyCoursesAsync(id.ToString()));

            return BadRequest(
                string.Format(Validations.LecturerValidationErorr.UserIsNotLecturer, id.ToString()));
        }

        [HttpGet]
        public IActionResult EditCourse(int courseId, string courseName)
        {
            var missionsVM = lecturerService.GetAllMissionsOfCourse(courseId, courseName);
            missionsVM.Choosen = new Exercise() { Id=1, GitHubLink="dd", Description ="dd", Title="d"};
            return View("EditCourse",missionsVM);

        }

        [HttpPost]
        public IActionResult EditCourse(ExerciseCourseVM ex)
        {
            if(!ModelState.IsValid)
            {
                return View("EditCourse", ex);
            }

            return Ok(ex.Choosen);

        }
    }
}