
namespace OnlineUniversity.Domain.Commands
{
    public class UpdateCourseStatisticsCommandResponse : CommandResponse
    {
        public UpdateCourseStatisticsCommandResponse(CourseStatistics courseStatistics)
        {
            this.CourseStatistics = courseStatistics;
        }

        public CourseStatistics CourseStatistics { get; private set; }
    }
}
