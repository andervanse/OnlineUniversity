
namespace OnlineUniversity.Domain.Commands
{
    public class UpdateCourseStatisticsCommand
    {
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
