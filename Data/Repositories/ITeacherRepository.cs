using System.Threading.Tasks;
using ClockIn_ClockOut.Data.Entities;

namespace ClockIn_ClockOut.Data.Repositories
{
    /// <summary>
    /// This interface knows how to access and manipulate data related to teachers.
    /// </summary>
    public interface ITeacherRepository
    {
        Task CreateTeacher(Teacher teacher);
        Teacher Find(int id);
        Teacher FindByUsername(string username);
    }
}