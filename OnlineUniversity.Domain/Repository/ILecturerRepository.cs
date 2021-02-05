
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineUniversity.Domain.Repository
{
    public interface ILecturerRepository
    {
        Task<bool> CreateAsync(Lecturer lecturer);
        Task<IEnumerable<Lecturer>> GetAllAsync();
        Task<Lecturer> GetByIdAsync(Guid id);
    }
}
