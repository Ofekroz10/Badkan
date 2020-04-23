using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Services;
using MyProject.ViewModel;

namespace MyProject.Controllers
{
    
    public class AdminManagerController : Controller
    {
        private readonly IUserService userService;

        public AdminManagerController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MakeUserLecturer()
        {
            if (User.IsInRole(Models.UserRollsType.Admin.IntValueAsString()))
                return View(userService.GetUsersByRolls());
            else
                return BadRequest();
        }

        public async Task<IActionResult> ChangePrivacy(string userId, UserRollsType to)
        {
            var result = await userService.ChangeRoleAsync(userId, to);
            if (result.Succeeded)
                return RedirectToAction("MakeUserLecturer");
            else
                return BadRequest();

        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            var lst = userService.GetAllLecturers();
            CourseViewModel courseViewModel = new CourseViewModel();
            courseViewModel.List = new List<IsCheckedLecturer>();
            
            foreach(var lecture in lst)
            {
                courseViewModel.List.Add(new IsCheckedLecturer
                {
                    IsChecked = false,
                    LecturerId = lecture.UserId
                });
            }


            return View(courseViewModel);
        }

        [HttpPost]
        public IActionResult AddCourse(CourseViewModel viewModel)
        {
            return View(viewModel);
        }



    }
}