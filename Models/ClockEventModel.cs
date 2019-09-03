using System;
using System.ComponentModel.DataAnnotations;

namespace ClockIn_ClockOut.Models
{
    public class ClockEventModel
    {
        public string TeacherName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EventDateTime { get; set; }
    }
}