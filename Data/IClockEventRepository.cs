using System.Collections.Generic;
using System.Threading.Tasks;
using ClockIn_ClockOut.Data.Entities;

namespace ClockIn_ClockOut.Data
{
    public interface IClockEventRepository
    {
        Task CreateClockEvent(ClockEvent clockEvent);
        ClockEvent Find(int id);
        List<ClockEvent> GetTeacherClockEvents(string teacherName);
        ClockEvent GetTeacherLastClockEvent(string teacherName);
        Task UpdateClockEvent(ClockEvent clockEvent);
    }
}