using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineUniversity.Domain;
using OnlineUniversity.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineUniversity.Data.EFCore
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<CourseRepository> _logger;

        public CourseRepository(
            DataContext context,
            ILogger<CourseRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddStudentToCourseAsync(Course course, Student student)
        {
            try
            {
                if (student.Id == null)
                {
                    await _context.Students.AddAsync(student);
                }
                else
                {
                    var studentDb = _context.Students.Where(s => s.Email == student.Email).FirstOrDefault();

                    if (studentDb == null)
                        await _context.Students.AddAsync(student);
                }

                var persistedCourse = await _context.Courses.FindAsync(course.Id);

                if (persistedCourse != null)
                {
                    persistedCourse.NumberOfStudents = course.NumberOfStudents;

                    var studentInCourse = _context.StudentCourses.Where(sc => sc.Student.Id == student.Id).FirstOrDefault();

                    if (studentInCourse == null)
                    {
                        await _context.StudentCourses.AddAsync(new StudentCourse { Course = persistedCourse, Student = student });
                    }
                    else
                    {
                        _logger.LogWarning("Student '{0}' alread exists in course '{1}'", student.Email, persistedCourse.Name);
                    }

                    _logger.LogTrace("Student email:'{0}' enrolled to course: '{1}'", student.Email, persistedCourse.Name);
                }
                else
                {
                    _logger.LogWarning("Course '{0}' not found.", course.Id);
                    return false;
                }

                var affectedRows = await _context.SaveChangesAsync();
                _logger.LogTrace("Student email:'{0}' enrolled to course: '{1}'", student.Email, persistedCourse.Name);
                return affectedRows > 0;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Problem to persist student: {0}, Course Id: {1}, Exception Message: {2}", student.Name, course.Id, e.Message);

                return false;
            }
        }

        public async Task<bool> CreateAsync(Course course)
        {
            try
            {
                await _context.Courses.AddAsync(course);
                var affectedRows = await _context.SaveChangesAsync();
                return affectedRows > 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Problem to persist course: {0}, Exception Message: {1}", course.Name, e.Message);
                return false;
            }
        }

        public async Task<Course> GetByIdAsync(Guid id)
        {
            try
            {
                var course = await _context.Courses
                    .Where(c => c.Id == id)
                    .Include(c => c.Lecturer)
                    .Include(c => c.Students)
                    .ThenInclude(s => s.Student)                    
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return course;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Problem to find student: {0}, Exception Message: {1}", id, e.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            try
            {
                var courses = await _context.Courses
                    .Include(c => c.Lecturer)
                    .AsNoTracking()
                    .ToListAsync();

                return courses;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Problem retrieving all courses, Exception Message: {0}", e.Message);
                return null;
            }
        }
     
    }
}
