using System.Collections.Generic;
using ClockIn_ClockOut.Data.Entities;

namespace ClockIn_ClockOut.Models
{
    public class ClockEventPageViewModel
    {
        /// <summary>
        /// Teacher associated with this event
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// List of clock events
        /// </summary>
        public List<ClockEvent> ClockEvents { get; set; }

        /// <summary>
        /// Used to show success messages.
        /// </summary>
        public string SuccessMessage { get; set; }

        /// <summary>
        /// Used to show general messages.
        /// </summary>
        public string Message { get; set; }
    }
}