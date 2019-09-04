using System.Diagnostics;
using ClockIn_ClockOut.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClockIn_ClockOut.Controllers
{
    /// <summary>
    /// Login page controller.
    /// </summary>
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
            return LocalRedirect(Url.Action(nameof(ClockEventsController.Index), "ClockEvents", new { teacherName = teacher.UserName }));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
