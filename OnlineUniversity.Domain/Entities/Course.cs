using System;
using System.Collections.Generic;

namespace OnlineUniversity.Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Lecturer Lecturer { get; set; }
        public int Capacity { get; set; }
        public int NumberOfStudents { get; set; }

        public bool IsFull
        {
            get
            {
                return NumberOfStudents >= Capacity;
            }
        }

        public ICollection<StudentCourse> Students { get; set; }
    }
}
