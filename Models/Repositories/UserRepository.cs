﻿using Microsoft.AspNetCore.Identity;
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

        public IEnumerable<IdentityUser> GetAll()
        {
            return userManager.Users;
        }

        public async Task<IdentityUser> GetByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityUser> GetById(string id)
        {
            return await userManager.FindByNameAsync(id);
        }

        public async Task<IdentityResult> UpdateUser(IdentityUser user)
        {
            return await userManager.UpdateAsync(user);
        }
    }
  }

