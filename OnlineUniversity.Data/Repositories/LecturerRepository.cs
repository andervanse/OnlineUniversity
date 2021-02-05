using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineUniversity.Domain;
using OnlineUniversity.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineUniversity.Data.EFCore
{
    public class LecturerRepository : ILecturerRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<LecturerRepository> _logger;

        public LecturerRepository(
            DataContext context,
            ILogger<LecturerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(Lecturer lecturer)
        {
            try
            {
                await _context.Lecturers.AddAsync(lecturer);
                var affectedRows = await _context.SaveChangesAsync();
                return affectedRows > 0;
            }
            catch (Exception e)
            {
                _logger.LogError("Problem to persist lecturer: {0}, Exception Message: {1}", lecturer.Name, e.Message);
                return false;
            }            
        }

        public async Task<Lecturer> GetByIdAsync(Guid id)
        {
            try
            {
                var lecturer = await _context.Lecturers.FindAsync(id);
                return lecturer;
            }
            catch (Exception e)
            {
                _logger.LogError("Problem to find lecturer: {0}, Exception Message: {1}", id, e.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Lecturer>> GetAllAsync()
        {
            try
            {
                var lecturers = await _context.Lecturers
                    .AsNoTracking()
                    .ToListAsync();

                return lecturers;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Problem retrieving all Lecturers, Exception Message: {0}", e.Message);
                return null;
            }
        }
    }
}
