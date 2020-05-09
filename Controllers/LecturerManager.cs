using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProject.Services;

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
    }
}