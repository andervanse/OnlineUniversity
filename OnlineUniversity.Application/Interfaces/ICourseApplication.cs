using OnlineUniversity.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineUniversity.Application.Interfaces
{
    public interface ICourseApplication
    {
        Task<ApplicationResponse<bool>> CreateAsync(CourseDto course);
        Task<ApplicationResponse<CourseDto>> GetByIdAsync(string courseId);
        Task<ApplicationResponse<IEnumerable<CourseDto>>> GetAllAsync();
    }
}
