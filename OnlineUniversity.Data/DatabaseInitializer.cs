using OnlineUniversity.Domain;
using System.Collections.Generic;
using System.Linq;

namespace OnlineUniversity.Data.EFCore
{
    public class DatabaseInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Lecturers.Any())
            {
                #region Lecturers
                var lecturers = new List<Lecturer>
                {
                    new Lecturer {
                        Name = "Montague Erma"
                    },
                    new Lecturer {
                        Name = "Melvyn Rosalyn"
                    },
                    new Lecturer {
                        Name = "Sera Daly"
                    }
                };
                context.Lecturers.AddRange(lecturers);
                #endregion

                #region Courses
                var courses = new List<Course>
                {
                    new Course {
                        Name = "Python",
                        Capacity = 5,
                        Lecturer = lecturers[0]
                    },
                    new Course {
                        Name = "Front-end Web Development",
                        Capacity = 5,
                        Lecturer = lecturers[1]
                    },
                    new Course {
                        Name = "Full-stack Developer",
                        Capacity = 5,
                        Lecturer = lecturers[2]
                    }
                };
                context.Courses.AddRange(courses);
                #endregion

                #region Students
                var students = new List<Student>
                {
                    new Student
                    {
                        Name = "Alexandria Lester Stoddard",
                        Email = "alexandria@gmail.com",
                        DateOfBirth = new System.DateTime(1994, 03, 11)
                    },
                    new Student
                    {
                        Name = "Ginny Alanna Jakeman",
                        Email = "ginny@gmail.com",
                        DateOfBirth = new System.DateTime(1999, 07, 20)
                    },
                    new Student
                    {
                        Name = "Gwenda Irvine Kendal",
                        Email = "gwenda@gmail.com",
                        DateOfBirth = new System.DateTime(2004, 03, 02)
                    },
                    new Student
                    {
                        Name = "Bridget Hattie Stanton",
                        Email = "bridget@gmail.com",
                        DateOfBirth = new System.DateTime(1987, 11, 15)
                    },
                    new Student
                    {
                        Name = "Averill Shanelle Forrest",
                        Email = "averill@gmail.com",
                        DateOfBirth = new System.DateTime(1990, 01, 09)
                    },
                    new Student
                    {
                        Name = "Diantha Blaine Bannerman",
                        Email = "diantha@gmail.com",
                        DateOfBirth = new System.DateTime(2002, 04, 26)
                    }
                };

                context.Students.AddRange(students);
                #endregion
            }

            context.SaveChanges();
        }
    }
}
