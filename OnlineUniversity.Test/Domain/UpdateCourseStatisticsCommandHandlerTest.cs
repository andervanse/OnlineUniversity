using Microsoft.Extensions.Logging;
using Moq;
using OnlineUniversity.Domain;
using OnlineUniversity.Domain.Commands;
using OnlineUniversity.Domain.Repository;
using OnlineUniversity.Infrastructure;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace OnlineUniversity.Test.Domain
{
    public class UpdateCourseStatisticsCommandHandlerTest
    {
        private readonly ITestOutputHelper output;
        private readonly ILogger<UpdateCourseStatisticsCommandHandler> logger;

        public UpdateCourseStatisticsCommandHandlerTest(ITestOutputHelper output)
        {
            var mock = new Mock<ILogger<UpdateCourseStatisticsCommandHandler>>();
            logger = mock.Object;
            this.output = output;
        }

        [Theory]
        [InlineData("2000-01-01", 0, 1, "C#")]
        [InlineData("2021-01-01", 34, 3, "Java")]
        [InlineData("1986-01-01", 349, 10, "Python")]
        public async Task Update_Course_Statistics_Average_Age_Test(string dateOfBirth, int sumAges, int numberOfStudents, string courseName)
        {
            var courseSt = new CourseStatistics
            {
                CourseId = Guid.NewGuid(),
                Name = courseName,
                SumAges = sumAges
            };

            var mock = new Mock<ICourseStatisticsRepository>();
            mock.Setup(r => r.GetByCourseIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(courseSt));

            mock.Setup(r => r.UpdateAsync(It.IsAny<CourseStatistics>()))
                .Returns(Task.FromResult(true));

            var signUpCommandHandler = new UpdateCourseStatisticsCommandHandler(mock.Object, logger);

            var updCourseStatisticsCommander = new UpdateCourseStatisticsCommand
            {
                Course = new Course { Id = courseSt.CourseId, Name = courseSt.Name, NumberOfStudents = numberOfStudents },
                Student = new Student { DateOfBirth = DateTime.Parse(dateOfBirth) }
            };

            var commandResponse = await signUpCommandHandler.Handle(updCourseStatisticsCommander);
            var age = new DateTime(DateTime.Now.Subtract(DateTime.Parse(dateOfBirth)).Ticks).Year - 1;
            var averageAge = (decimal)(sumAges + age) / numberOfStudents;

            output.WriteLine("Course statistics: {0}, Average Age: {1}", commandResponse.CourseStatistics.Name, commandResponse.CourseStatistics.AverageAge);
            Assert.Equal(age, updCourseStatisticsCommander.Student.Age);
            Assert.Equal(averageAge, commandResponse.CourseStatistics.AverageAge);
        }
       
    }
}
