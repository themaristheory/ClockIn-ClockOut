using System.Collections.Generic;
using System.Threading.Tasks;
using ClockIn_ClockOut.Data.Entities;

namespace ClockIn_ClockOut.Data.Repositories
{
    /// <summary>
    /// This interface knows how to access and manipulate data related to clock events.
    /// </summary>
    public interface IClockEventRepository
    {
        Task CreateClockEvent(ClockEvent clockEvent);
        Task DeleteClockEvent(int id);
        ClockEvent Find(int id);
        List<ClockEvent> GetTeacherClockEvents(string teacherName);
        ClockEvent GetTeacherLastClockEvent(string teacherName);
        Task UpdateClockEvent(ClockEvent clockEvent);
    }
}