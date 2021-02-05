using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineUniversity.Domain;
using OnlineUniversity.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUniversity.Data.EFCore
{
    public class CourseStatisticsRepository : ICourseStatisticsRepository
    {
        private readonly DataContext _context;

        public CourseStatisticsRepository(
            DataContext context,
            ILogger<CourseStatisticsRepository> logger)
        {
            _context = context;
        }

        public async Task<bool> UpdateAsync(CourseStatistics courseStatistics)
        {
            var foundEntity = await _context.CourseStatistics.FindAsync(courseStatistics.CourseId);

            if (foundEntity == null)
            {
                _context.CourseStatistics.Add(courseStatistics);
            }
            else
            {
                _context.CourseStatistics.Attach(courseStatistics);
                _context.Entry<CourseStatistics>(courseStatistics).State = EntityState.Modified;
            }

            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<IEnumerable<CourseStatistics>> GetAllAsync()
        {
            return await _context.CourseStatistics.AsNoTracking().ToListAsync();
        }

        public async Task<CourseStatistics> GetByCourseIdAsync(Guid courseId)
        {
            return await _context.CourseStatistics.FindAsync(courseId);
        }
    }
}
