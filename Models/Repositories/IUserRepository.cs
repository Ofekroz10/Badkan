using Microsoft.AspNetCore.Identity;
using MyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Reposetories
{
    public interface IUserRepository
    {
        public IEnumerable<IdentityUser> GetAll();
        public Task<IdentityUser> GetById(string id);
        public Task<IdentityUser> GetByEmail(string email);
        public Task<IdentityResult> AddUserWithPassword(IdentityUser user, string password);
        public Task<IdentityResult> ChangePassword(IdentityUser user, string oldPassword, string newPassword);
        public Task<IdentityResult> UpdateUser(IdentityUser user);
        public PasswordVerificationResult ComparePasswords(IdentityUser user, string newPassword);
    }
}
