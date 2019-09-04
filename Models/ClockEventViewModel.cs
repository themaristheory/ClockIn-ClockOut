using System;
using ClockIn_ClockOut.Data.Entities;

namespace ClockIn_ClockOut.Models
{
    public class ClockEventViewModel
    {
        public int Id { get; set; }

        public Teacher Teacher { get; set; }

        public DateTime EventDateTime { get; set; }

        public bool ClockIn { get; set; }

        public string Message { get; set; }
    }
}