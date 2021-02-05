using OnlineUniversity.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineUniversity.Application
{
    public interface ICourseStatisticsApplication
    {
        Task<IEnumerable<CourseStatisticsDto>> GetAllAsync();
    }
}
