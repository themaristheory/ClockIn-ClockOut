using System;

namespace ClockIn_ClockOut.Data.Entities
{
    public class ClockEvent
    {
        public int Id { get; set; }

        public Teacher Teacher { get; set; }

        public DateTime EventDateTime { get; set; }

        public bool ClockIn { get; set; }
    }
}