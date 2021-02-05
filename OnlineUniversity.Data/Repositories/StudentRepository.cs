using Microsoft.Extensions.Logging;
using OnlineUniversity.Domain;
using OnlineUniversity.Domain.Repository;
using System;
using System.Threading.Tasks;

namespace OnlineUniversity.Data.EFCore
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<StudentRepository> _logger;

        public StudentRepository(
            DataContext context,
            ILogger<StudentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(Student student)
        {
            try
            {
                await _context.Students.AddAsync(student);
                var affectedRows = await _context.SaveChangesAsync();
                return affectedRows > 0;
            }
            catch (Exception e)
            {
                _logger.LogError("Problem to persist student: {0}, Exception Message: {1}", student.Name, e.Message);
                return false;
            }
        }

        public async Task<Student> GetByIdAsync(Guid id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                return student;
            }
            catch (Exception e)
            {
                _logger.LogError("Problem to find student: {0}, Exception Message: {1}", id, e.Message);
                return null;
            }
        }
    }
}
