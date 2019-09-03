using System;
using System.ComponentModel.DataAnnotations;

namespace ClockIn_ClockOut.Models
{
    public class ClockEventModel
    {
        /// <summary>
        /// Teacher associated with this event
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// Whether this event is a clock-in (true) or out (false)
        /// </summary>
        public bool ClockIn { get; set; }

        /// <summary>
        /// Date and time of this event
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime EventDateTime { get; set; }

        /// <summary>
        /// Used to show success messages.
        /// </summary>
        public string SuccessMessage { get; set; }
    }
}