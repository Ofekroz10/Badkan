using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyProject.Dtos;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public interface IUserService
    {
        /*Validations*/
        public Task<bool> IsOldPasswordValidAsync(string oldPassword, long id);
        public Task<bool> CheckExistByEmail(string email);
        public Task<bool> CheckExistById(string id);

        /*Reposetory*/
        public IEnumerable<IdentityUser> GetAll();
        public Task<IdentityUser> GetById(string id);
        public Task<IdentityUser> GetByEmail(string email);
        public Task<IdentityResult> AddUserWithPassword(IdentityUser user, string password);
        public Task<IdentityResult> ChangePassword(IdentityUser user, string oldPassword, string newPassword);
        public Task<IdentityResult> UpdateUser(IdentityUser user);
        public PasswordVerificationResult ComparePasswords(IdentityUser user, string newPassword);
        public Task<IdentityResult> DeleteUser(IdentityUser user);


        /*Api Functions */

        public IList<UserDto> MakeUserDto(IEnumerable<IdentityUser> users);
        public UserDto MakeUserDto(IdentityUser user);
        public Task<IdentityResult> CreateRole(string name);
        public Task<IdentityResult> RegisterUserToRole(int userId, string roleName);
        public Task<bool> CheckIfRoleExist(string roleName);
        public IEnumerable<IdentityRole> GetListOfRoles();
        public Task<bool> CheckIfUserBelongToRole(string userId, string roleName);
        public IEnumerable<IdentityUserRole<string>> GetUserRolesTable();
        public IEnumerable<IdentityRole> GetAllRolls();
        public IEnumerable GetUsersByRolls();

}
}