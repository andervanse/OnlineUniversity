using System;
using System.Collections.Generic;

namespace OnlineUniversity.Domain
{
    public class Lecturer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
