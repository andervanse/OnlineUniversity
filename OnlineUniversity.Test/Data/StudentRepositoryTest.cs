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
    public class StudentRepositoryTest
    {
        private readonly ITestOutputHelper output;
        private readonly DataContext context;
        private readonly ILogger<StudentRepository> logger;

        public StudentRepositoryTest(
            ITestOutputHelper output,
            ContextFixture ctxFixture)
        {
            this.output = output;
            context = ctxFixture.context;
            var mock = new Mock<ILogger<StudentRepository>>();
            logger = mock.Object;
        }

        [Fact]
        public async Task Create_Student_Test()
        {
            StudentRepository repository = new StudentRepository(context, logger);

            var student = new Student
            {
                Name = "Jocelin Malachi Paige",
                Email = "jocelin@gmail.com",
                DateOfBirth = new DateTime(2000, 12, 16)
            };

            var persisted = await repository.CreateAsync(student);

            Assert.True(persisted);
            Assert.True(student.Id != null);
        }

        [Fact]
        public async Task Get_Students_By_Id_Test()
        {
            StudentRepository repository = new StudentRepository(context, logger);

            var student = new Student
            {
                Name = "Kenneth Hammond Wilkie",
                Email = "kenneth@gmail.com",
                DateOfBirth = new DateTime(1988, 08, 05)
            };

            await repository.CreateAsync(student);
            var persistedStudent = await repository.GetByIdAsync(student.Id);
            output.WriteLine("Student: {0}, Birth: {1}, Age: {2}", persistedStudent.Name, persistedStudent.DateOfBirth.ToShortDateString(), persistedStudent.Age);

            Assert.True(persistedStudent != null);
        }

    }
}
