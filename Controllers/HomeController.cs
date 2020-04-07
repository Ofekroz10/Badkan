using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyProject.Models;
using MyProject.Models.Testing;

namespace MyProject.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IManager manager;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            manager =  new Manager("checking");
            /*
            try
            {
                
                manager.GitClone("https://github.com/Ofekroz10/MyWebExample.git");
                manager.Compile();
                string eror = manager.GetCurEror();
                System.Diagnostics.Debug.WriteLine("Eror of compile: " + eror);
                manager.Run();
                string erorOfRunning = manager.GetCurEror();
                System.Diagnostics.Debug.WriteLine("Eror of running: " + eror);
                string output = manager.GetCurOutput();
                System.Diagnostics.Debug.WriteLine("output of running: " + output);
                manager.DeleteDir();
            }
            catch(GitCloneException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            */
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
