
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineUniversity.Application.DTOs
{
    public class CourseDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public LecturerDto Lecturer { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Capacity { get; set; }

        public int NumberOfStudents { get; set; }
    }
}
