using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineUniversity.Domain.Repository
{
    public interface ICourseStatisticsRepository
    {
        Task<bool> UpdateAsync(CourseStatistics courseStatistics);

        Task<IEnumerable<CourseStatistics>> GetAllAsync();

        Task<CourseStatistics> GetByCourseIdAsync(Guid courseId);
    }
}
