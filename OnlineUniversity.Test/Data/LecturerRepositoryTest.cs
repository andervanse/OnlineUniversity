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
    public class LecturerRepositoryTest
    {
        private readonly ITestOutputHelper output;
        private readonly DataContext context;
        private readonly ILogger<LecturerRepository> logger;

        public LecturerRepositoryTest(
            ITestOutputHelper output,
            ContextFixture ctxFixture)
        {
            this.output = output;
            context = ctxFixture.context;
            var mock = new Mock<ILogger<LecturerRepository>>();
            logger = mock.Object;
        }

        [Fact]
        public async Task Create_Lecturer_Test()
        {
            LecturerRepository repository = new LecturerRepository(context, logger);

            var lecturer = new Lecturer
            {
                Name = "Jocelin Malachi Paige"
            };

            var persisted = await repository.CreateAsync(lecturer);

            Assert.True(persisted);
            Assert.True(lecturer.Id != null);
        }

        [Fact]
        public async Task Get_All_Lecturers_Test()
        {
            LecturerRepository repository = new LecturerRepository(context, logger);

            var list = await repository.GetAllAsync();

            foreach (var lecturer in list)
            {
                output.WriteLine("Lecturer: {0}", lecturer.Name);
            }

            Assert.True(list.Count() > 0);
        }

    }
}
