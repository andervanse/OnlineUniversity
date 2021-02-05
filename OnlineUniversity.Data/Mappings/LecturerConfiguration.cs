using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineUniversity.Domain;


namespace OnlineUniversity.Data.EFCore.Mappings
{
    public class LecturerConfiguration : IEntityTypeConfiguration<Lecturer>
    {
        public void Configure(EntityTypeBuilder<Lecturer> builder)
        {
            builder.ToTable("Lecturers");
            builder.HasKey(cp => cp.Id);
            builder.Property(cp => cp.Name).HasMaxLength(200).IsRequired();
        }
    }
}
