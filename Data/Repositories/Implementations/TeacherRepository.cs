using System.Linq;
using System.Threading.Tasks;
using ClockIn_ClockOut.Data.Entities;

namespace ClockIn_ClockOut.Data.Repositories.Implementations
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ClockSystemContext _context;

        public TeacherRepository(
            ClockSystemContext context)
        {
            _context = context;
        }

        public Teacher Find(int id)
        {
            return _context.Teachers.FirstOrDefault(t => t.Id == id);
        }

        public Teacher FindByUsername(string username)
        {
            return _context.Teachers.FirstOrDefault(t => t.UserName.Equals(username));
        }

        public async Task CreateTeacher(Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }
    }
}
