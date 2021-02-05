using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineUniversity.Domain;

namespace OnlineUniversity.Data.EFCore.Mappings
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(cp => cp.Id);
            builder.Property(cp => cp.Name).HasMaxLength(200).IsRequired();
            builder.Property(cp => cp.Email).IsRequired();
            builder.Property(cp => cp.DateOfBirth).IsRequired();
        }
    }
}
