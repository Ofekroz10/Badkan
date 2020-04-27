﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyProject.Models;
using MyProject.Services;
using MyProject.Validations;
using MyProject.ViewModel;

namespace MyProject.Controllers
{
    
    public class AdminManagerController : Controller
    {
        private readonly IUserService userService;
        private readonly IAdminService adminService;



        public AdminManagerController(IUserService userService, IAdminService adminService)
        {
            this.userService = userService;
            this.adminService = adminService;
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
        public async Task<IActionResult> AddCourseAsync(CourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int courseId = await adminService.AddCourse(viewModel);
                    viewModel.Course.CourseId = courseId;
                }
                catch(ArgumentException e)
                {
                    ModelState.AddModelError("",string.Format(AdminValidations.COURSE_EXIST, viewModel.Course.CourseName));
                    return View(viewModel);
                }

                adminService.AddLecturersToCourse(viewModel.GetAllSelectedLecturers());

                MessagesViewModel msg = new MessagesViewModel();
                msg.Messages.Add("Course "+ viewModel.Course.CourseName + " added");


                return RedirectToAction("Index", "Home", msg);
            }

                return View(viewModel);
            
        }



    }
}