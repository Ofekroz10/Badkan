using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class MyDbContext : IdentityDbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<CourseLecturers> CourseLecturers { get; set; }
        public DbSet<ExerciseCourses> ExercisesCourses { get; set; }


        public MyDbContext(DbContextOptions<MyDbContext> option)
            :base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CourseLecturers>().HasKey(cl => new { cl.CourseId, cl.LecturerId });
            modelBuilder.Entity<ExerciseCourses>().HasKey(ec => new { ec.CourseId, ec.ExId });

            foreach (var item in UserRollsTypeExtentions.AsCollection())
                modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                {
                    Name = item.IntValueAsString(),
                    NormalizedName = item.IntValueAsString().ToUpper()
                }); ;
        }

    }
    
}
