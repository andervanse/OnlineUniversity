using Microsoft.Extensions.Logging;
using Moq;
using OnlineUniversity.Data.EFCore;
using OnlineUniversity.Domain;
using OnlineUniversity.Test.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace OnlineUniversity.Test
{
    [Collection("Database collection")]
    public class CourseRepositoryTest
    {
        private readonly ITestOutputHelper output;
        private readonly DataContext context;
        private readonly ILogger<CourseRepository> logger;

        public CourseRepositoryTest(
            ITestOutputHelper output,
            ContextFixture ctxFixture)
        {
            this.output = output;
            context = ctxFixture.context;
            var mock = new Mock<ILogger<CourseRepository>>();
            logger = mock.Object;
        }

        [Fact]
        public async Task Add_And_Get_Course_Test()
        {
            CourseRepository repository = new CourseRepository(context, logger);

            var course = new Course
            {
                Name = "Test 001",
                Lecturer = new Lecturer { Name = "Petra Oswald Cross" },
                Capacity = 5,
                NumberOfStudents = 5
            };

            await repository.CreateAsync(course);
            var persistedCourse = await repository.GetByIdAsync(course.Id);

            Assert.NotNull(persistedCourse);
        }

        [Fact]
        public async Task Add_Student_To_Course_Test()
        {
            CourseRepository repository = new CourseRepository(context, logger);

            var student = new Student
            {
                Name = "Sara Bailey Winston",
                Email = "sara.bailey@gmail.com",
                DateOfBirth = new DateTime(2000, 03, 12)               
            };

            var courses = await repository.GetAllAsync();
            var firstCourse = courses.FirstOrDefault();
            await repository.AddStudentToCourseAsync(firstCourse, student);
            var persistedCourse = await repository.GetByIdAsync(firstCourse.Id);
            
            Assert.NotNull(persistedCourse);
            Assert.True(persistedCourse.Students.Count > 0);
        }

        [Fact]
        public async Task Add_Student_To_Invalid_Course_Test()
        {
            CourseRepository repository = new CourseRepository(context, logger);

            var student = new Student
            {
                Name = "Sara Bailey Winston",
                Email = "sara.bailey@gmail.com",
                DateOfBirth = new System.DateTime(2000, 03, 12)
            };

            var course = new Course { Id = Guid.NewGuid(), Name = "Invalid subject", Capacity = 0 };
            var result = await repository.AddStudentToCourseAsync(course, student);

            Assert.False(result);
        }
    }
}
