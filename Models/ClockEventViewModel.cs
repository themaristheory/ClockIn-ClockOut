using System;
using ClockIn_ClockOut.Data.Entities;

namespace ClockIn_ClockOut.Models
{
    /// <summary>
    /// Model used to show a clock event in its edit view.
    /// </summary>
    public class ClockEventViewModel
    {
        public int Id { get; set; }

        public Teacher Teacher { get; set; }

        public DateTime EventDateTime { get; set; }

        public bool ClockIn { get; set; }

        public string Message { get; set; }
    }
}