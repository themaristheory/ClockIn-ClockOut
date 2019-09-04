using System;
using System.Threading.Tasks;
using ClockIn_ClockOut.Data.Entities;
using ClockIn_ClockOut.Data.Repositories;
using ClockIn_ClockOut.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClockIn_ClockOut.Controllers
{
    public class ClockEventsController : Controller
    {
        private Teacher LoggedInTeacher;

        private readonly IClockEventRepository _clockEventRepository;
        private readonly ITeacherRepository _teacherRepository;

        public ClockEventsController(
            IClockEventRepository clockEventRepository,
            ITeacherRepository teacherRepository)
        {
            _clockEventRepository = clockEventRepository;
            _teacherRepository = teacherRepository;
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

            var clockEvents = _clockEventRepository.GetTeacherClockEvents(teacherName);

            return View("Index", new ClockEventPageViewModel
            {
                TeacherName = LoggedInTeacher.UserName,
                ClockEvents = clockEvents,
				Message = clockEvents.Count > 0 ? null : "No clock events yet!"
			});
        }

        [HttpGet(UriTemplates.ClockEvents_Create)]
        public async Task<IActionResult> Create(string teacherName)
        {
            RecoverLoggedInTeacher(teacherName);

            var now = DateTime.Now;
            var lastClockEvent = _clockEventRepository.GetTeacherLastClockEvent(teacherName);

            await _clockEventRepository.CreateClockEvent(new ClockEvent
            {
                ClockIn = !lastClockEvent?.ClockIn ?? true, // the next clock event is always the opposite of the last saved one OR it is the first clock in
                EventDateTime = now,
                Teacher = LoggedInTeacher,
            });

            return View("Index", new ClockEventPageViewModel
            {
                TeacherName = LoggedInTeacher.UserName,
                SuccessMessage = "Clock event registered!"
            });
        }

        [HttpGet(UriTemplates.ClockEvents_Edit)]
        public IActionResult Edit(int id, string teacherName)
        {
            RecoverLoggedInTeacher(teacherName);

            var clockEvent = _clockEventRepository.Find(id);

            return View("EditClockEvent", new ClockEventViewModel
            {
                Id = clockEvent.Id,
                Teacher = LoggedInTeacher,
                EventDateTime = clockEvent.EventDateTime,
                ClockIn = clockEvent.ClockIn,
            });
        }

        [HttpPost(UriTemplates.ClockEvents_Edit)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClockEvent clockEvent)
        {
            if (id != clockEvent.Id)
            {
                return NotFound();
            }

            clockEvent.Teacher = _teacherRepository.FindByUsername(clockEvent.Teacher.UserName);

            if (ModelState.IsValid)
            {
                try
                {
                    await _clockEventRepository.UpdateClockEvent(clockEvent);
                    return BuildEditClockView(clockEvent, "Event updated successfully!");
                }
                catch (Exception)
                {
                    return BuildEditClockView(clockEvent, "There was a problem!");
                }
            }

            return BuildEditClockView(clockEvent, "There was a problem!");
        }

        private ViewResult BuildEditClockView(ClockEvent clockEvent, string message)
        {
            return View("EditClockEvent", new ClockEventViewModel
            {
                Id = clockEvent.Id,
                Teacher = clockEvent.Teacher,
                EventDateTime = clockEvent.EventDateTime,
                ClockIn = clockEvent.ClockIn,
                Message = message
            });
        }

        private void RecoverLoggedInTeacher(string teacherName)
        {
            LoggedInTeacher = _teacherRepository.FindByUsername(teacherName);

            if (LoggedInTeacher == null)
            {
                var userName = teacherName.Replace(" ", "");

                _teacherRepository.CreateTeacher(new Teacher
                {
                    UserName = userName,
                    FullName = teacherName
                });

                LoggedInTeacher = _teacherRepository.FindByUsername(userName);
            }
        }
    }
}
