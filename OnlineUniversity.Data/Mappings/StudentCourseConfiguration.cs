using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineUniversity.Domain;


namespace OnlineUniversity.Data.EFCore.Mappings
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder.HasOne(sc => sc.Student)
                   .WithMany(sc => sc.Courses)
                   .HasForeignKey(sc => sc.CourseId);

            builder.HasOne(sc => sc.Course)
                   .WithMany(sc => sc.Students)
                   .HasForeignKey(sc => sc.StudentId);
        }
    }
}
