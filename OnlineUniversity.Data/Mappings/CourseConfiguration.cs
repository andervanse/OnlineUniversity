using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineUniversity.Domain;

namespace OnlineUniversity.Data.EFCore.Mappings
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(cp => cp.Id);
            builder.Property(cp => cp.Name).HasMaxLength(200).IsRequired();
            builder.Property(cp => cp.Capacity).IsRequired();
            builder.HasOne(c => c.Lecturer);
        }
    }
}
