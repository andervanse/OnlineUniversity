using OnlineUniversity.Application.DTOs;
using System;
using System.Threading.Tasks;

namespace OnlineUniversity.Application.Interfaces
{
    public interface ILecturerApplication
    {
        Task<ApplicationResponse<bool>> CreateAsync(LecturerDto lecturer);

        Task<ApplicationResponse<LecturerDto>> GetByIdAsync(string lecturerId);
    }
}
