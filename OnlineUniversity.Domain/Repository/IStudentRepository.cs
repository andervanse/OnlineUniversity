
using System;
using System.Threading.Tasks;

namespace OnlineUniversity.Domain.Repository
{
    public interface IStudentRepository
    {
        Task<bool> CreateAsync(Student student);

        Task<Student> GetByIdAsync(Guid id);
    }
}
