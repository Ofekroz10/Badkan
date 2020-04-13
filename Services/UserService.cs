using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyProject.Dtos;
using MyProject.Models.Reposetories;
using MyProject.Services;

namespace MyProject.Services
{
    public class UserService : IUserService
    {
        IUserRepository userRepo;

        public UserService(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public async Task<bool> IsOldPasswordValidAsync(string oldPassword, long id)
        {
            var user = await userRepo.GetById(id.ToString());
            return userRepo.ComparePasswords(user, oldPassword) == PasswordVerificationResult.Success;
        }

        public async Task<bool> CheckExistByEmail(string email)
        {
            return await userRepo.GetByEmail(email) != null;
        }

        public IEnumerable<IdentityUser> GetAll()
        {
            return userRepo.GetAll();
        }

        public async Task<IdentityUser> GetById(string id)
        {
            return await userRepo.GetById(id);
        }

        public async Task<IdentityUser> GetByEmail(string email)
        {
            return await userRepo.GetByEmail(email);
        }

        public async Task<IdentityResult> AddUserWithPassword(IdentityUser user, string password)
        {
            return await userRepo.AddUserWithPassword(user, password);
        }

        public async Task<IdentityResult> ChangePassword(IdentityUser user, string oldPassword, string newPassword)
        {
            return await userRepo.ChangePassword(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> UpdateUser(IdentityUser user)
        {
            return await userRepo.UpdateUser(user);
        }

        public PasswordVerificationResult ComparePasswords(IdentityUser user, string newPassword)
        {
            return userRepo.ComparePasswords(user, newPassword);
        }

        public async Task<IdentityResult> DeleteUser(IdentityUser user)
        {
            return await userRepo.DeleteUser(user);
        }

        public IList<UserDto> MakeUserDto(IEnumerable<IdentityUser> users)
        {
            IList<UserDto> userDtoList = new List<UserDto>();

            foreach (var user in users)
            {
                userDtoList.Add(MakeUserDto(user));
            }

            return userDtoList;
        }

        public UserDto MakeUserDto(IdentityUser user)
        {
            UserDto newUser = new UserDto { Id = int.Parse(user.UserName), Email = user.Email };
            var hasher = new PasswordHasher<IdentityUser>();
            return newUser;
        }
    }
}
