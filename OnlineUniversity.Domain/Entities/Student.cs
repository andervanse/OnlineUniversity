using System;
using System.Collections.Generic;

namespace OnlineUniversity.Domain
{
    public class Student
    {
        public Student() { }

        public Student(string name, string email, DateTime dateOfBirth) 
        {
            Name = name;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get
            {
                return new DateTime(DateTime.Now.Subtract(DateOfBirth).Ticks).Year - 1;
            }
        }

        public ICollection<StudentCourse> Courses { get; set; }
    }
}
