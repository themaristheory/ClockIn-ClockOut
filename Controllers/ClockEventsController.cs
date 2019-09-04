using System;
using System.Linq;
using System.Threading.Tasks;
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

            return View(new ClockEventPageViewModel
            {
                TeacherName = LoggedInTeacher.UserName
            });
        }

        [HttpGet(UriTemplates.ClockEvents_List)]
        public IActionResult List(string teacherName)
        {
            RecoverLoggedInTeacher(teacherName);

            var clockEvents = _context.ClockEvents.Where(c => c.Teacher.UserName.Equals(LoggedInTeacher.UserName)).ToList();

            return View("Index", new ClockEventPageViewModel
            {
                TeacherName = LoggedInTeacher.UserName,
                ClockEvents = clockEvents,
            });
        }

        [HttpGet(UriTemplates.ClockEvents_Create)]
        public async Task<IActionResult> Create(string teacherName)
        {
            RecoverLoggedInTeacher(teacherName);

            var now = DateTime.Now;
            var lastClockEvent = _context.ClockEvents.Where(c => c.Teacher.UserName.Equals(LoggedInTeacher.UserName)).LastOrDefault();

            await _context.ClockEvents.AddAsync(new ClockEvent
            {
                ClockIn = !lastClockEvent?.ClockIn ?? true, // the next clock event is always the opposite of the last saved one OR it is the first clock in
                EventDateTime = now,
                Teacher = LoggedInTeacher,
            });
            await _context.SaveChangesAsync();

            return View("Index", new ClockEventPageViewModel
            {
                TeacherName = LoggedInTeacher.UserName,
                SuccessMessage = "Clock event registered!"
            });
        }

        private void RecoverLoggedInTeacher(string teacherName)
        {
            LoggedInTeacher = _context.Teachers.FirstOrDefault(t => t.UserName.Equals(teacherName));

            if (LoggedInTeacher == null)
            {
                var userName = teacherName.Replace(" ", "");

                _context.Teachers.AddAsync(new Teacher
                {
                    UserName = userName,
                    FullName = teacherName
                });
                _context.SaveChanges();

                LoggedInTeacher = _context.Teachers.FirstOrDefault(t => t.UserName.Equals(userName));
            }
        }
    }
}
