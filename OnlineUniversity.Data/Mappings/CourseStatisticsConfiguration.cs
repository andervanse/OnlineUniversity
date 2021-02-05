using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineUniversity.Domain;

namespace OnlineUniversity.Data.EFCore.Mappings
{
    public class CourseStatisticsConfiguration : IEntityTypeConfiguration<CourseStatistics>
    {
        public void Configure(EntityTypeBuilder<CourseStatistics> builder)
        {
            builder.ToTable("CourseStatistics");
            builder.HasKey(cp => cp.CourseId);
            builder.Property(cp => cp.Name).HasMaxLength(200).IsRequired();
            builder.Property(cp => cp.MinimumAge).IsRequired();
            builder.Property(cp => cp.MaximumAge).IsRequired();
            builder.Property(cp => cp.AverageAge).IsRequired();
            builder.Property(cp => cp.SumAges).IsRequired();
        }
    }
}
