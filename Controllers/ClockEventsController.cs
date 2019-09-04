using System;
using System.Threading.Tasks;
using ClockIn_ClockOut.Data.Entities;
using ClockIn_ClockOut.Data.Repositories;
using ClockIn_ClockOut.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClockIn_ClockOut.Controllers
{
    /// <summary>
    /// Clock events controller.
    /// </summary>
    public class ClockEventsController : Controller
    {
        // Used to keep the logged in teacher while actual login is not implemented.
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

        /// <summary>
        /// Returns application's main page.
        /// </summary>
        [HttpGet(UriTemplates.ClockEvents)]
        public IActionResult Index(string teacherName)
        {
            RecoverLoggedInTeacher(teacherName);

            return View(new ClockEventPageViewModel
            {
                TeacherName = LoggedInTeacher.UserName
            });
        }

        /// <summary>
        /// Returns all clock events of a teacher.
        /// </summary>
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

        /// <summary>
        /// Creates a clock event with the current date, time, and teacher.
        /// Whether the event is a clock in or a clock out is controlled based on the last event of the same teacher.
        /// </summary>
        [HttpGet(UriTemplates.ClockEvents_Create)]
        public async Task<IActionResult> Create(string teacherName)
        {
            RecoverLoggedInTeacher(teacherName);

            var now = DateTime.Now;
            var lastClockEvent = _clockEventRepository.GetTeacherLastClockEvent(teacherName);

            await _clockEventRepository.CreateClockEvent(new ClockEvent
            {
                ClockIn = !lastClockEvent?.ClockIn ?? true, // the next clock event is always the opposite of the last saved event OR it is the first clock in
                EventDateTime = now,
                Teacher = LoggedInTeacher,
            });

            return View("Index", new ClockEventPageViewModel
            {
                TeacherName = LoggedInTeacher.UserName,
                SuccessMessage = "Clock event registered!"
            });
        }

        /// <summary>
        /// Returns the edit clock event view.
        /// </summary>
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

        /// <summary>
        /// Edits a clock event.
        /// Only the time of the clock event can be edited.
        /// </summary>
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

        /// <summary>
        /// Deletes a clock event.
        /// </summary>
        [HttpGet(UriTemplates.ClockEvents_Delete)]
        public async Task<IActionResult> Delete(int id, string teacherName)
        {
            RecoverLoggedInTeacher(teacherName);

            await _clockEventRepository.DeleteClockEvent(id);

            return View("Index", new ClockEventPageViewModel
            {
                TeacherName = LoggedInTeacher.UserName,
                ClockEvents = _clockEventRepository.GetTeacherClockEvents(teacherName),
                SuccessMessage = "Clock event deleted!"
            });
        }

        /// <summary>
        /// Builds and return the view to edit a clock event.
        /// </summary>
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

        /// <summary>
        /// Since login system is not implemented yet, recover teacher from database or create a teacher, if no teacher is found with the required username.
        /// </summary>
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
