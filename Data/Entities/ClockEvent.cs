using System;

namespace ClockIn_ClockOut.Data.Entities
{
    /// <summary>
    /// This is the ClockEvent entity. It represents a clock event in the database.
    /// </summary>
    public class ClockEvent
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Teacher who registered this clock event.
        /// </summary>
        public Teacher Teacher { get; set; }

        /// <summary>
        /// Date and time of this clock event.
        /// </summary>
        public DateTime EventDateTime { get; set; }

        /// <summary>
        /// Whether this is a clock in (TRUE) or out (FALSE).
        /// </summary>
        public bool ClockIn { get; set; }
    }
}