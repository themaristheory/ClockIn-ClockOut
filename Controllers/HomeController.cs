using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClockIn_ClockOut.Models;

namespace ClockIn_ClockOut.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(TeacherModel teacher)
        {
            // pretend the teacher logged in and redirect to the view with the logic for clock in and clock out
            return LocalRedirect(Url.Action(nameof(ClockEventsController.Index), "ClockEvents", new { teacherName = teacher.FullName }));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
