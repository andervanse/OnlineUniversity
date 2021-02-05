using Microsoft.Extensions.Logging;
using OnlineUniversity.Domain.Commands.Interfaces;
using OnlineUniversity.Domain.Repository;
using OnlineUniversity.Domain.Validation;
using OnlineUniversity.Infrastructure;
using OnlineUniversity.Infrastructure.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineUniversity.Domain.Commands
{
    public class SignUpToCourseCommandHandler : ISignUpToCourseCommandHandler
    {
        private readonly ICourseRepository _repository;
        private readonly ILogger<SignUpToCourseCommandHandler> _logger;

        public SignUpToCourseCommandHandler(
            ICourseRepository courseRepository, 
            ILogger<SignUpToCourseCommandHandler> logger)
        {
            _repository = courseRepository;
            _logger = logger;
        }

        public async Task<SignUpToCourseCommandResponse> Handle(SignUpToCourseCommand command)
        {
            var student = new Student(command.Name, command.Email, command.DateOfBirth);
            var studentValidator = new StudentValidator();
            var validationResult = studentValidator.Validate(student);
            var response = new SignUpToCourseCommandResponse();

            if (validationResult.IsValid)
            {
                var course = await _repository.GetByIdAsync(command.CourseId);

                if (course == null)
                {
                    response.Message = "Course not found.";
                    response.AddError($"Course Id '{command.CourseId}' not found.");
                }
                else if (course.IsFull)
                {
                    response.Message = "Course max students number reached.";
                    response.AddError("Sorry, We this course is closed for new students.");
                }
                else
                {
                    course.NumberOfStudents += 1;
                    var success = await _repository.AddStudentToCourseAsync(course, student);

                    if (success)
                    {
                        response.Message = "OK";
                        response.Course = course;
                        response.Student = student;
                    }

                    if (!success)
                    {
                        response.Message = "Oops, Something went wrong.";
                        response.AddError("Sorry, We had a problem enrolling you to this course, try later.");
                    }
                }
            }
            else
            {
                response.Message = "Oops, Something went wrong.";
                response.AddErrorsRange(validationResult.Errors.Select(v => v.ErrorMessage));
            }

            return response;
        }
    }
}
