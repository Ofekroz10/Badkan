using Microsoft.AspNetCore.Identity;
using MyProject.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public interface IUserService
    {
        /*Validations*/
        public Task<bool> IsOldPasswordValidAsync(string oldPassword, long id);
        public Task<bool> CheckExistByEmail(string email);

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
        //public IList<string> GetErorsFromModelState(ModelStateDictionary m)

}
}