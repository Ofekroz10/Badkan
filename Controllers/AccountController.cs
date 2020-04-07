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

namespace MyProject.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<IdentityUser> signInManager;
        private IUserRepository userRepo;


        public AccountController(SignInManager<IdentityUser> singInManager, IUserRepository userRepo)
        {
            this.signInManager = singInManager;
            this.userRepo = userRepo;
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

                if (await CheckExistByEmail(user.Email))
                {
                    ModelState.AddModelError("", string.Format(UserValidationErors.EMAIL_EXIST_EROR_MSG,user.Email));
                    return View(model);
                }

                var result = await userRepo.AddUserWithPassword(user, model.Password);

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

        private async Task<bool> CheckExistByEmail(string email)
        {
            return await userRepo.GetByEmail(email) != null;
        }
    }
}