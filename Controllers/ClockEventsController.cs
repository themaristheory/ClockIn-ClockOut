using System;
using System.Linq;
using ClockIn_ClockOut.Data;
using ClockIn_ClockOut.Data.Entities;
using ClockIn_ClockOut.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClockIn_ClockOut.Controllers
{
    public class ClockEventsController : Controller
    {
        private Teacher LoggedInTeacher;
        private ClockSystemContext _context;

        public ClockEventsController(
            ClockSystemContext context)
        {
            _context = context;
        }

        [HttpGet(UriTemplates.ClockEvents)]
        public IActionResult Index(string teacherName)
        {
            RecoverLoggedInTeacher(teacherName);

            return View(new ClockEventModel
            {
                TeacherName = teacherName
            });
        }

        private void RecoverLoggedInTeacher(string teacherName)
        {
            LoggedInTeacher = _context.Teachers.FirstOrDefault(t => t.UserName.Equals(teacherName));

            if (LoggedInTeacher == null)
            {
                LoggedInTeacher = new Teacher
                {
                    FullName = teacherName
                };
            }
        }
    }
}
