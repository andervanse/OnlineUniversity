using Microsoft.Extensions.Logging;
using Moq;
using OnlineUniversity.Data.EFCore;
using OnlineUniversity.Domain;
using OnlineUniversity.Test.Data;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace OnlineUniversity.Test
{
    [Collection("Database collection")]
    public class CourseStatisticsRepositoryTest
    {
        private readonly ITestOutputHelper output;
        private readonly DataContext context;
        private readonly ILogger<CourseStatisticsRepository> logger;

        public CourseStatisticsRepositoryTest(
            ITestOutputHelper output,
            ContextFixture ctxFixture)
        {
            this.output = output;
            context = ctxFixture.context;
            var mock = new Mock<ILogger<CourseStatisticsRepository>>();
            logger = mock.Object;
        }

        [Fact]
        public async Task Update_CourseStatistics_Test()
        {
            CourseStatisticsRepository courseStatisticsRepository = new CourseStatisticsRepository(context, logger);
            var courseLoggerMock = new Mock<ILogger<CourseRepository>>();
            CourseRepository courseRepository = new CourseRepository(context, courseLoggerMock.Object);
            var courses = await courseRepository.GetAllAsync();

            bool allPersisted = true;

            foreach (var course in courses)
            {
                var courseStatistics = new CourseStatistics { CourseId = course.Id, Name = course.Name };
                var persisted = await courseStatisticsRepository.UpdateAsync(courseStatistics);

                if (!persisted)
                {
                    allPersisted = false;
                    break;
                }
            }

            Assert.True(allPersisted);
        }

        [Fact]
        public async Task Get_CourseStatistics_Test()
        {
            CourseStatisticsRepository repository = new CourseStatisticsRepository(context, logger);
            var courseLoggerMock = new Mock<ILogger<CourseRepository>>();
            CourseRepository courseRepository = new CourseRepository(context, courseLoggerMock.Object);
            var courses = await courseRepository.GetAllAsync();
            var firstCourse = courses.FirstOrDefault();
            var statistics = await repository.GetByCourseIdAsync(firstCourse.Id);
            output.WriteLine("Course Statistics: {0}", statistics.Name);

            Assert.True(statistics != null);
        }
    }
}
