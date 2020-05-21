using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Dtos;
using MyProject.Services;

namespace MyProject.Controllers.Api
{
    [Route("api/lecture")]
    [ApiController]
    public class LectureController : ControllerBase
    {
        private readonly IAdminService adminService;

        public LectureController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet("MissionDetails")]
        public ExerciseDto GetExercise(int id)
        {
            var ex = adminService.GetExerciseById(id);
            ExerciseDto result = new ExerciseDto()
            {
                Title = ex.Title,
                Description = ex.Description,
                GitHubLink = ex.GitHubLink
            };

            return result;
        }

    }
}