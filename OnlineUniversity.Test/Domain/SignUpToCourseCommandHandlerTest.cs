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
    public class SignUpToCourseCommandHandlerTest
    {
        private readonly ITestOutputHelper output;
        private readonly ILogger<SignUpToCourseCommandHandler> logger;

        public SignUpToCourseCommandHandlerTest(ITestOutputHelper output)
        {
            var mock = new Mock<ILogger<SignUpToCourseCommandHandler>>();
            logger = mock.Object;
            this.output = output;
        }

        [Theory]
        [InlineData( 1, 1, false)]
        [InlineData( 5, 5, false)]
        [InlineData( 5, 4, true)]
        [InlineData(10, 3, true)]
        public async Task Enroll_Student_When_Course_Is_Full(int capacity, int numberOfStudents, bool isValid)
        {
            var course = new Course
            {
                Id = Guid.NewGuid(),                
                Name = "Mock Test",
                Capacity = capacity,
                NumberOfStudents = numberOfStudents
            };
            var mock = new Mock<ICourseRepository>();
            mock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(course));
            mock.Setup(r => r.AddStudentToCourseAsync(It.IsAny<Course>(), It.IsAny<Student>()))
                .Returns(Task.FromResult(true));

            var courseRepository = mock.Object;
            var signUpCommandHandler = new SignUpToCourseCommandHandler(courseRepository, logger);

            var sigUpCommander = new SignUpToCourseCommand
            {
                CourseId = course.Id,
                Name = "Stafford Loren Forrest",
                Email = "stafford@gmail.com",
                DateOfBirth = new DateTime(1999, 04, 19)
            };

            var commandResponse = await signUpCommandHandler.Handle(sigUpCommander);

            output.WriteLine("Command Response Message: {0}, is valid: {1}, Error: {2}", commandResponse.Message, commandResponse.Succeeded, string.Join(", ", commandResponse.Errors));
            Assert.Equal(isValid, commandResponse.Succeeded);
        }

        [Theory]
        [InlineData(2, 1, 2)]
        [InlineData(5, 3, 4)]
        public async Task Increase_Number_Of_Students_Course(int capacity, int numberOfStudents, int numberOfStudentsAfter)
        {
            var course = new Course
            {
                Id = Guid.NewGuid(),
                Name = "Mock Test",
                Capacity = capacity,
                NumberOfStudents = numberOfStudents
            };
            var mock = new Mock<ICourseRepository>();
            mock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(course));
            mock.Setup(r => r.AddStudentToCourseAsync(It.IsAny<Course>(), It.IsAny<Student>()))
                .Returns(Task.FromResult(true));

            var courseRepository = mock.Object;
            var signUpCommandHandler = new SignUpToCourseCommandHandler(courseRepository, logger);

            var sigUpCommander = new SignUpToCourseCommand
            {
                CourseId = course.Id,
                Name = "Stafford Loren Forrest",
                Email = "stafford@gmail.com",
                DateOfBirth = new DateTime(1999, 04, 19)
            };

            var commandResponse = await signUpCommandHandler.Handle(sigUpCommander);

            output.WriteLine("Command Response Message: {0}, is valid: {1}, Error: {2}", commandResponse.Message, commandResponse.Succeeded, string.Join(", ", commandResponse.Errors));
            Assert.Equal(numberOfStudentsAfter, commandResponse.Course.NumberOfStudents);
        }


    }
}
