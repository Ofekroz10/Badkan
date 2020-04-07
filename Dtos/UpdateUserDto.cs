using MyProject.Dtos;

namespace MyProject.Controllers.Api
{
    public class UpdateUserDto : RegisterUserDto
    {
        public string OldPassword { get; set; }
    }
}