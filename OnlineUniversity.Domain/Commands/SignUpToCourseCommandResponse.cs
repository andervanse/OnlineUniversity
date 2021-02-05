
namespace OnlineUniversity.Domain.Commands
{
    public class SignUpToCourseCommandResponse : CommandResponse
    {
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
