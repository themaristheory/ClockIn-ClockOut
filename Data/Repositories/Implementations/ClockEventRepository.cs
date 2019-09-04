using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClockIn_ClockOut.Data.Entities;

namespace ClockIn_ClockOut.Data.Repositories.Implementations
{
    public class ClockEventRepository : IClockEventRepository
    {
        private ClockSystemContext _context;

        public ClockEventRepository(
            ClockSystemContext context)
        {
            _context = context;
        }

        public ClockEvent Find(int id)
        {
            return _context.ClockEvents.FirstOrDefault(c => c.Id == id);
        }

        public List<ClockEvent> GetTeacherClockEvents(string teacherName)
        {
            return _context.ClockEvents
                .Where(c => c.Teacher.UserName.Equals(teacherName))
                .OrderByDescending(c => c.EventDateTime)
                .ToList();
        }

        public ClockEvent GetTeacherLastClockEvent(string teacherName)
        {
            return _context.ClockEvents.Where(c => c.Teacher.UserName.Equals(teacherName)).LastOrDefault();
        }

        public async Task CreateClockEvent(ClockEvent clockEvent)
        {
            await _context.ClockEvents.AddAsync(clockEvent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClockEvent(ClockEvent clockEvent)
        {
            _context.ClockEvents.Update(clockEvent);
            await _context.SaveChangesAsync();
        }
    }
}
