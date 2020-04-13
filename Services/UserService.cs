using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyProject.Dtos;
using MyProject.Models.Reposetories;
using MyProject.Services;
using MyProject.Validations;

namespace MyProject.Services
{
    public class UserService : IUserService
    {
        IUserRepository userRepo;
        RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UserService(IUserRepository userRepo, RoleManager<IdentityRole> roleManager,
                    SignInManager<IdentityUser> signInManager)
        {
            this.userRepo = userRepo;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
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

        public async Task<IdentityResult> CreateRole(string name)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = name
            };

            var result = await roleManager.CreateAsync(identityRole);

            return result;

        }

        public async Task<IdentityResult> RegisterUserToRole(int userId, string roleName)
        {
            var user = await GetById(userId.ToString());
            var result = await userRepo.AddRoleToUser(user, roleName);
            return result;

        }

        public async Task<bool> CheckIfRoleExist(string roleName)
        {
            return await roleManager.RoleExistsAsync(roleName);
        }

        public async Task<bool> CheckExistById(string id)
        {
            return await GetById(id) != null; 
        }

        public IEnumerable<IdentityRole> GetListOfRoles()
        {
            IEnumerable<IdentityRole> rolesLst = roleManager.Roles;
            return rolesLst;
        }

        public async Task<bool> CheckIfUserBelongToRole(string userId, string roleName)
        {
            return await userRepo.IsUserBelongToRole(userId, roleName);
        }

        public IEnumerable<IdentityUserRole<string>> GetUserRolesTable()
        {
            return userRepo.GetUserRols().ToList();
        }

        public IEnumerable<IdentityRole> GetAllRolls()
        {
            return roleManager.Roles;
        }

        public IEnumerable GetUsersByRolls()
        {
            var userRoles = GetUserRolesTable().ToList();
            var users = GetAll();
            var rols = GetAllRolls();

            var afterJoin = from t1 in users
                            join ur in userRoles on t1.Id equals ur.UserId
                            select new { t1.UserName, ur.RoleId };

            var afterJoinRole = from t1 in afterJoin
                                join ro in rols on t1.RoleId equals ro.Id
                                orderby ro.Id
                                select new { t1.UserName, ro.Name };

            return afterJoinRole;
        }
    }
}
