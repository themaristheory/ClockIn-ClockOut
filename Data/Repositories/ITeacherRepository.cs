using System.Threading.Tasks;
using ClockIn_ClockOut.Data.Entities;

namespace ClockIn_ClockOut.Data.Repositories
{
    public interface ITeacherRepository
    {
        Task CreateTeacher(Teacher teacher);
        Teacher Find(int id);
        Teacher FindByUsername(string username);
    }
}