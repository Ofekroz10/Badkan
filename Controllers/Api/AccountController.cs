using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyProject.Dtos;
using MyProject.Models;
using MyProject.Models.Reposetories;
using MyProject.Services;
using MyProject.Validations;
using PasswordVerificationResult = Microsoft.AspNetCore.Identity.PasswordVerificationResult;

namespace MyProject.Controllers.Api
{
    [AllowAnonymous]
    [Route(ApiRoutes.AccountRoutes.ROOT)]
    [ApiController]
    [System.Runtime.InteropServices.Guid("5CDF4174-F4B6-425C-93DC-531C93F776A3")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(IUserService userService, SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userService = userService;
        }

        [HttpGet(ApiRoutes.AccountRoutes.ALL_USERS)]
        public IEnumerable<UserDto> GetAllUsers()
        {
            return userService.MakeUserDto(userService.GetAll());
        }


        [HttpGet(ApiRoutes.AccountRoutes.SPECIFIC_USER)]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var user = await userService.GetById(id.ToString());

            if (user == null)
                return BadRequest(string.Format(UserValidationErors.USER_DO_NOT_EXIST, id.ToString()));

            return Ok(userService.MakeUserDto(user));
        }

        [HttpPost(ApiRoutes.AccountRoutes.CREATE_USER)]
        public async Task<IActionResult> CreateUser(RegisterUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = userDto.Id.ToString(), Email = userDto.Email };

                if (await userService.CheckExistByEmail(user.Email))
                    return BadRequest(
                         string.Format(UserValidationErors.EMAIL_EXIST_EROR_MSG, user.Email));

                var result = await userService.AddUserWithPassword(user, userDto.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return Ok();
                }

                foreach (var eror in result.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }

                return BadRequest(ModelState);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut(ApiRoutes.AccountRoutes.UPDATE_USER)]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var userFromDb = await userService.GetById(id.ToString());

                if (userFromDb == null)
                    return BadRequest(
                        string.Format(UserValidationErors.USER_DO_NOT_EXIST, id.ToString()));

                if (await userService.CheckExistByEmail(userDto.Email) && userDto.Email != userFromDb.Email)
                    return BadRequest(
                        string.Format(UserValidationErors.EMAIL_EXIST_EROR_MSG, userDto.Email));

                if (await userService.GetById(userDto.ToString()) != null)
                    return BadRequest(
                        string.Format(UserValidationErors.USER_ALREADY_EXIST, userDto.Id));


                PasswordVerificationResult passResult = userService.ComparePasswords(userFromDb, userDto.OldPassword);

                if (passResult == PasswordVerificationResult.Success)
                {
                    var result = await userService.ChangePassword(userFromDb, userDto.OldPassword, userDto.Password);

                    if (!result.Succeeded)
                        return BadRequest(result.Errors);
                }
                else
                    return BadRequest(UserValidationErors.INCORRECT_PASSWORD);

                userFromDb.Email = userDto.Email;
                userFromDb.UserName = userDto.Id.ToString();
                var resultAfterUpdate = await userService.UpdateUser(userFromDb);

                if (!resultAfterUpdate.Succeeded)
                    BadRequest(resultAfterUpdate.Errors);

                return Ok();

            }

            return BadRequest(ModelState);

        }

        [HttpDelete(ApiRoutes.AccountRoutes.DELETE_USER)]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var user = await userService.GetById(id.ToString());
            if (user == null)
                return BadRequest(UserValidationErors.USER_DO_NOT_EXIST);

            var result = await userService.DeleteUser(user);
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }

        [HttpPost(ApiRoutes.AccountRoutes.CREATE_ROLE)]
        public async Task<IActionResult> CreateRole([FromBody]AddUserToRoleDto rolldto)
        {
            string roleName = "";
            try
            {
                roleName = ((UserRollsType)(rolldto.Role)).IntValueAsString();
            }
            catch (Exception e)
            {
                return BadRequest(string.Format(UserValidationErors.IS_NOT_IN_ENUM, rolldto.Role));
            }

            var result = await userService.CreateRole(roleName);
            if (result.Succeeded)
                return Ok();
            return BadRequest(result.Errors);

        }

        [HttpGet(ApiRoutes.AccountRoutes.GET_USER_ROLS)]
        public IActionResult GetUsersByRoles()
        {
            var userRollsLst = userService.GetUsersByRolls();
            return Ok(userRollsLst);

        }

        [HttpPost(ApiRoutes.AccountRoutes.ADD_USER_TO_ROLE)]
        public async Task<IActionResult> AddUserToRole(int userId, [FromBody]AddUserToRoleDto roleDto)
        {
            string roleName = "";
            try
            {
                roleName = ((UserRollsType)(roleDto.Role)).IntValueAsString();
            }
            catch(Exception e)
            {
                return BadRequest(string.Format(UserValidationErors.IS_NOT_IN_ENUM, roleDto.Role));
            }

            if (await userService.CheckIfRoleExist(roleName))
            {
                if (await userService.CheckExistById(userId.ToString()))
                {
                    var result = await userService.RegisterUserToRole(userId, roleName);
                    if (!result.Succeeded)
                        ModelState.AddModelError("", string.Join(",", result.Errors));
                }
                else
                    ModelState.AddModelError("", string.Format(UserValidationErors.ROLE_DO_NOT_EXIST, roleName));
            }
            else
                ModelState.AddModelError("", string.Format(UserValidationErors.USER_DO_NOT_EXIST, userId));

            if (ModelState.IsValid)
                return Ok();
            return BadRequest(ModelState);

        }
    }
}