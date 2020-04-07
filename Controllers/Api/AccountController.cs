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
using MyProject.Models.Reposetories;
using MyProject.Validations;
using PasswordVerificationResult = Microsoft.AspNetCore.Identity.PasswordVerificationResult;

namespace MyProject.Controllers.Api
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    [System.Runtime.InteropServices.Guid("5CDF4174-F4B6-425C-93DC-531C93F776A3")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository userRepo;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(IUserRepository userRepo, SignInManager<IdentityUser> signInManager)
        {
            this.userRepo = userRepo;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IEnumerable<UserDto> GetAllUsers()
        {
            return MakeUserDto(userRepo.GetAll());
        }


        [HttpGet("{id}")]
        public async Task<UserDto> GetUserAsync(int id)
        {
            var user = await userRepo.GetById(id.ToString());
            return MakeUserDto(user);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateUser(RegisterUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = userDto.Id.ToString(), Email = userDto.Email };

                if (await CheckExistByEmail(user.Email))
                    return CreateHttpMessage(HttpStatusCode.BadRequest,
                         string.Format(UserValidationErors.EMAIL_EXIST_EROR_MSG, user.Email));

                var result = await userRepo.AddUserWithPassword(user, userDto.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return CreateHttpMessage(HttpStatusCode.OK);
                }

                foreach (var eror in result.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }

            }
            return CreateHttpMessage(HttpStatusCode.OK);
        }

        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> UpdateUser(int id, UpdateUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var userFromDb = await userRepo.GetById(id.ToString());

                if (userFromDb == null)
                    return CreateHttpMessage(HttpStatusCode.NotFound,
                        string.Format(UserValidationErors.USER_DO_NOT_EXIST, id.ToString()));

                if (await CheckExistByEmail(userDto.Email) && userDto.Email != userFromDb.Email)
                    return CreateHttpMessage(HttpStatusCode.BadRequest, 
                        string.Format(UserValidationErors.EMAIL_EXIST_EROR_MSG, userDto.Email));

                if(await userRepo.GetById(userDto.ToString())!=null)
                    return CreateHttpMessage(HttpStatusCode.BadRequest, 
                        string.Format(UserValidationErors.USER_ALREADY_EXIST, userDto.Id));


                PasswordVerificationResult passResult = userRepo.ComparePasswords(userFromDb, userDto.OldPassword);

                if (passResult == PasswordVerificationResult.Success)
                {
                    var result = await userRepo.ChangePassword(userFromDb, userDto.OldPassword, userDto.Password);

                    if (!result.Succeeded)
                        return CreateHttpMessage(HttpStatusCode.BadRequest, string.Join(',',result.Errors));
                }
                else
                    return CreateHttpMessage(HttpStatusCode.BadRequest, UserValidationErors.INCORRECT_PASSWORD);

                userFromDb.Email = userDto.Email;
                userFromDb.UserName = userDto.Id.ToString();
                var resultAfterUpdate = await userRepo.UpdateUser(userFromDb);

                if(!resultAfterUpdate.Succeeded)
                    return CreateHttpMessage(HttpStatusCode.BadRequest, string.Join(',', resultAfterUpdate.Errors));

                return CreateHttpMessage(HttpStatusCode.OK);

            }

            return CreateHttpMessage(HttpStatusCode.BadRequest, string.Join(",",ModelState.ToArray()));

        }


        private IList<UserDto> MakeUserDto(IEnumerable<IdentityUser> users)
        {
            IList<UserDto> userDtoList = new List<UserDto>();

            foreach (var user in users)
            {
                userDtoList.Add(MakeUserDto(user));
            }

            return userDtoList;
        }
        private UserDto MakeUserDto(IdentityUser user)
        {
            UserDto newUser = new UserDto { Id = int.Parse(user.UserName), Email = user.Email };
            var hasher = new PasswordHasher<IdentityUser>();
            return newUser;
        }
        private async Task<bool> CheckExistByEmail(string email)
        {
            return await userRepo.GetByEmail(email) != null;
        }
        private IList<string> GetErorsFromModelState(ModelStateDictionary m)
        {
            var errors = new List<string>();
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            return errors;
        }
        private HttpResponseMessage CreateHttpMessage(HttpStatusCode status, string eror= "")
        {
            return new HttpResponseMessage
            {
                StatusCode = status,
                ReasonPhrase = eror
                };
            }
    }
}