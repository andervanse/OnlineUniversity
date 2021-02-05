using Microsoft.Extensions.Logging;
using OnlineUniversity.Domain.Commands.Interfaces;
using OnlineUniversity.Domain.Repository;
using System.Threading.Tasks;

namespace OnlineUniversity.Domain.Commands
{
    public class UpdateCourseStatisticsCommandHandler : IUpdateCourseStatisticsCommandHandler
    {
        private readonly ICourseStatisticsRepository _courseStatisticsRepository;
        private readonly ILogger<UpdateCourseStatisticsCommandHandler> _logger;

        public UpdateCourseStatisticsCommandHandler(
            ICourseStatisticsRepository repository,
            ILogger<UpdateCourseStatisticsCommandHandler> logger)
        {
            _courseStatisticsRepository = repository;
            _logger = logger;
        }

        public async Task<UpdateCourseStatisticsCommandResponse> Handle(UpdateCourseStatisticsCommand command)
        {
            var courseStatistics = await _courseStatisticsRepository.GetByCourseIdAsync(command.Course.Id);
            var student = command.Student;
            var course = command.Course;
            var numberOfStudents = course.NumberOfStudents > 0 ? course.NumberOfStudents : 1;

            if (courseStatistics != null)
            {
                courseStatistics.SumAges += student.Age;

                if (courseStatistics.MinimumAge > student.Age)
                    courseStatistics.MinimumAge = student.Age;

                if (courseStatistics.MaximumAge < student.Age)
                    courseStatistics.MaximumAge = student.Age;

                courseStatistics.AverageAge = (decimal)courseStatistics.SumAges / numberOfStudents;
            }
            else
            {
                courseStatistics = new CourseStatistics
                {
                    CourseId = course.Id,
                    Name = course.Name,
                    MinimumAge = student.Age,
                    MaximumAge = student.Age,
                    SumAges = student.Age,
                    AverageAge = (decimal)student.Age / numberOfStudents
                };
            }

            var success = await _courseStatisticsRepository.UpdateAsync(courseStatistics);
            var response = new UpdateCourseStatisticsCommandResponse(courseStatistics);

            if (success)
            {
                response.Message = "OK";
            }
            else
            {
                response.Message = "Sorry, Something went wrong updating course statistics.";
                var msgError = $"Course Statistics Name '{courseStatistics.Name}', Sum Ages {courseStatistics.SumAges}, Average Age {courseStatistics.AverageAge}, Student '{student.Email}'.";
                response.AddError(msgError);
                _logger.LogError(msgError);
            }

            return response;
        }
    }
}
