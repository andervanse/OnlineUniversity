
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineUniversity.Domain.Repository
{
    public interface ICourseRepository
    {
        Task<bool> CreateAsync(Course course);
        Task<Course> GetByIdAsync(Guid id);
        Task<bool> AddStudentToCourseAsync(Course course, Student student);
        Task<IEnumerable<Course>> GetAllAsync();
    }
}
