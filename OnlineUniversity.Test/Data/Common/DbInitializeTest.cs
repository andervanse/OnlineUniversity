using Microsoft.EntityFrameworkCore;
using OnlineUniversity.Data.EFCore;
using OnlineUniversity.Test.Data;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace OnlineUniversity.Test
{
    [Collection("Database collection")]
    public class DbInitializeTest
    {
        private readonly ITestOutputHelper output;
        private readonly DataContext context;

        public DbInitializeTest(
            ITestOutputHelper output,
            ContextFixture ctxFixture)
        {
            this.output = output;
            this.context = ctxFixture.context;
        }

        [Fact]
        public void DataContext_Test()
        {
            var lecturers = context.Lecturers.AsNoTracking().ToList();
            var courses = context.Courses.Include(c => c.Lecturer).AsNoTracking().ToList();
            var students = context.Students.AsNoTracking().ToList();

            Assert.True(lecturers.Any());
            Assert.True(courses.Any());
            Assert.True(students.Any());
        }
    }
}
