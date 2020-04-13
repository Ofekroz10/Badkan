using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Reposetories
{
    public class UserRepository : IUserRepository
    {
        
        private MyDbContext db;
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;



        public UserRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> singInManager,
                              MyDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = singInManager;
            this.db = db;
        }

        public async Task<IdentityResult> AddRoleToUser(IdentityUser user, string roleName)
        {
            return await userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> AddUserWithPassword(IdentityUser user, string password)
        {
            var result = await userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<IdentityResult> ChangePassword(IdentityUser user, string oldPassword, string newPassword)
        {
            return await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public PasswordVerificationResult ComparePasswords(IdentityUser user, string password)
        {
            return userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        }

        public async Task<IdentityResult> DeleteUser(IdentityUser user)
        {
            var result = await userManager.DeleteAsync(user);
            return result;
        }

        public IEnumerable<IdentityUser> GetAll()
        {
            return userManager.Users.ToList();
        }

        public async Task<IdentityUser> GetByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityUser> GetById(string id)
        {
            return await userManager.FindByNameAsync(id);
        }

        public IEnumerable<IdentityUserRole<string>> GetUserRols()
        {
            return db.UserRoles.ToList();
        }

        public async Task<bool> IsUserBelongToRole(string userId, string roleName)
        {
            var user = await GetById(userId);
            return await userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> UpdateUser(IdentityUser user)
        {
            return await userManager.UpdateAsync(user);
        }

    }
  }

