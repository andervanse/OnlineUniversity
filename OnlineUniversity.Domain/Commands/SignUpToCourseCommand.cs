using System;

namespace OnlineUniversity.Domain.Commands
{
    public class SignUpToCourseCommand
    {
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
