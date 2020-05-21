using MyProject.Models;
using MyProject.Models.Repositories;
using MyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class AdminService : IAdminService
    {
        private readonly ICourseLecturerRepository adminRepo;
        IUserService userService;

        public AdminService(ICourseLecturerRepository adminRepo, IUserService userService)
        {
            this.adminRepo = adminRepo;
            this.userService = userService;
        }

        public async Task<int> AddCourse(CourseViewModel courseVM)
        {
            if (adminRepo.GetCourseByName(courseVM.Course.CourseName) != null)
                throw new ArgumentException();

            Course course = courseVM.Course;
            int id  = await adminRepo.AddCourse(course);
            return id;
        }

        public async Task AddLecturersToCourseAsync(IList<CourseLecturers> courseLecturers)
        {
            await adminRepo.AddLecturersToCourse(courseLecturers);
        }

        public CourseViewModel GetCourseViewModel(string courseName)
        {
            var course = adminRepo.GetCourseByName(courseName);
            var lecturersOfThisCourse = adminRepo.GetLecturersOfCourseAsString(course);
            var allLecturers = userService.GetAllLecturers().Select(x=>x.UserId.ToString());

            List<IsCheckedLecturer> listToVm = new List<IsCheckedLecturer>();
            foreach(var lecture in allLecturers)
            {
                bool isChecked = false;

                if (lecturersOfThisCourse.SingleOrDefault(x => x.Equals(lecture)) != null)
                    isChecked = true;

                listToVm.Add(new IsCheckedLecturer() { LecturerId = lecture, IsChecked = isChecked });
            }

            CourseViewModel courseVM = new CourseViewModel
            {
                Course = course,
                List = listToVm
            };

            return courseVM;
        }

        public async Task<int> EditCourseAsync(CourseViewModel courseVM)
        {
            if (!CourseNameDontExistExceptFor(courseVM.Course.CourseName, courseVM.Course.CourseName)) throw new ArgumentException();
            if (await adminRepo.EditNameToCourseAsync(courseVM.Course) < 0) throw new SystemException();
            if (await adminRepo.DeleteAllLecturerOfCourse(courseVM.Course) < 0) throw new SystemException();
            var lstOfLecturers = courseVM.List.Where(item => item.IsChecked).ToList().Select(x => x.LecturerId);
            if (await adminRepo.AddLecturersToCourse(CreateCourseLecFromStrLst(lstOfLecturers, courseVM.Course.CourseId))
                < 0) throw new SystemException();

            return 1;
            
            
        }

        private IList<CourseLecturers> CreateCourseLecFromStrLst(IEnumerable<string> lst, int courseId)
        {
            IList<CourseLecturers> lstCourseLecturers = new List<CourseLecturers>();
            foreach (var x in lst)
            {
                lstCourseLecturers.Add(new CourseLecturers()
                {
                    LecturerId = int.Parse(x),
                    CourseId = courseId
                });
            }

            return lstCourseLecturers;
        }

        private bool CourseNameDontExistExceptFor(string exceptFor, string courseName)
        {
            var allCoursesName = adminRepo.GetAllCoursesName();
            var course = allCoursesName.SingleOrDefault(x => x == courseName);
            if (course == null || course.Equals(courseName))
                return true;
            return false;
        }


        public IEnumerable<CourseViewModel> GetUserLecturerWithCourseName()
        {
            var courses = adminRepo.GetAllCourses();
            IList<CourseViewModel> vm = new List<CourseViewModel>();

            foreach (var c in courses)
                vm.Add(CourseViewModel.
                    CreateVMfromLecturersCourse(c, adminRepo.GetLecturersOfCourseAsString(c)));
            return vm;
        }

        public async Task<int> DeleteCourse(string courseName)
        {
            Course c = adminRepo.GetCourseByName(courseName);
            IEnumerable<CourseLecturers> rows = adminRepo.GetLecturersOfCourse(c);
            return await adminRepo.DeleteCourseLecturerAndCourse(c, rows);
        }

        public Exercise GetExerciseById(int exId)
        {
            return adminRepo.GetExerciseById(exId);
        }
    }
}
