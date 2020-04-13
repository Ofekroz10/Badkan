using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Models.Reposetories;
using MyProject.ViewModel;
using MyProject.Validations;
using Microsoft.AspNetCore.Authorization;
using MyProject.Services;

namespace MyProject.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<IdentityUser> signInManager;
        private IUserService userService;

        public AccountController(SignInManager<IdentityUser> singInManager, IUserRepository userRepo, 
            IUserService userService)
        {
            this.signInManager = singInManager;
            this.userService = userService;
        }
        

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Id.ToString(), model.Password,
                    model.RememberMe, false);

                if(result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", UserValidationErors.LOGIN_FAILED_EROR_MSG);
            }


            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Id.ToString(), Email = model.Email };

                if (await userService.CheckExistByEmail(user.Email))
                {
                    ModelState.AddModelError("", string.Format(UserValidationErors.EMAIL_EXIST_EROR_MSG,user.Email));
                    return View(model);
                }

                var result = await userService.AddUserWithPassword(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);

                    if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Home");
                }

                foreach(var eror in result.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }

            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");

        }


        [HttpGet]
        public async Task<IActionResult> EditUser(long id)
        {
            var user = await userService.GetById(id.ToString());
            if (user == null)
                return BadRequest();

            UpdateUserViewModel updateViewModel = new UpdateUserViewModel
            {
                Email = user.Email,
                Id = long.Parse(user.UserName),
                PreId = long.Parse(user.UserName),
                NewPassword = "If you dont want to change, enter your current password",
                OldPassword = "If you dont want to change, enter your current password"

            };

            return View("Edit", updateViewModel);
            //create view and http post to the view, then chack validation with the attribute
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(UpdateUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var userFromDb = await userService.GetById(user.PreId.ToString());

                if (await userService.GetById(user.Id.ToString()) != null && user.Id != user.PreId)
                    ModelState.AddModelError("", 
                        string.Format(UserValidationErors.USER_ALREADY_EXIST, user.Id.ToString()));

                if (await userService.CheckExistByEmail(user.Email) && !user.Email.Equals(userFromDb.Email))
                    ModelState.AddModelError("", 
                        string.Format(UserValidationErors.EMAIL_EXIST_EROR_MSG, user.Email));

                if (!await userService.IsOldPasswordValidAsync(user.OldPassword, user.PreId))
                    ModelState.AddModelError("", UserValidationErors.INCORRECT_PASSWORD);

                if (ModelState.IsValid)
                {

                    var result = await userService.ChangePassword(userFromDb, user.OldPassword, user.NewPassword);

                    if (!result.Succeeded)
                        ModelState.AddModelError("", string.Join(",", result.Errors));
                    else
                    {
                        userFromDb.Email = user.Email;
                        userFromDb.UserName = user.Id.ToString();
                        var resultAfterUpdate = await userService.UpdateUser(userFromDb);

                        if (!resultAfterUpdate.Succeeded)
                            ModelState.AddModelError("", string.Join(",", resultAfterUpdate.Errors));
                        else
                        {
                            var updatedUser = await userService.GetById(user.Id.ToString());
                            await signInManager.RefreshSignInAsync(updatedUser);

                            MessagesViewModel msg = new MessagesViewModel();
                            msg.Messages.Add("User edit succses");

                            return RedirectToAction("Index", "Home",msg);
                        }
                    }
                }
            }
          
            return View("Edit", user);
        }
    }


}