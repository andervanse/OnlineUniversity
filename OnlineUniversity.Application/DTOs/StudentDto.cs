using System;

namespace OnlineUniversity.Application.DTOs
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
