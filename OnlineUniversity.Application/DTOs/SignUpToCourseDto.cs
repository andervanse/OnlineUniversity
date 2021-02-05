
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineUniversity.Application.DTOs
{
    public class SignUpToCourseDto
    {
        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public StudentDto Student { get; set; }
    }
}
