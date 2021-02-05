using AutoMapper;
using OnlineUniversity.Application.DTOs;
using OnlineUniversity.Application.Interfaces;
using OnlineUniversity.Domain.Commands;
using OnlineUniversity.Domain.Commands.Interfaces;
using OnlineUniversity.Infrastructure;
using OnlineUniversity.Infrastructure.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnlineUniversity.Application
{
    public class SignUpToCourseApplication : ISignUpToCourseApplication
    {
        private readonly ISignUpToCourseCommandHandler _signUpToCourseHandler;
        private readonly IUpdateCourseStatisticsCommandHandler _updateCourseStatisticsHandler;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public SignUpToCourseApplication(
            IMapper mapper,
            IEmailSender emailSender,
            ISignUpToCourseCommandHandler signUpToCourseHandler,
            IUpdateCourseStatisticsCommandHandler updateCourseStatisticsHandler)
        {
            _mapper = mapper;
            _emailSender = emailSender;
            _signUpToCourseHandler = signUpToCourseHandler;
            _updateCourseStatisticsHandler = updateCourseStatisticsHandler;            
        }

        public async Task<ApplicationResponse<bool>> SignUpStudentToCourseAsync(Guid courseId, StudentDto student)
        {

            var appResp = new ApplicationResponse<bool>();
            var command = _mapper.Map<SignUpToCourseCommand>(student);
            command.CourseId = courseId;
            var signUpToCourseCommandResponse = await _signUpToCourseHandler.Handle(command);

            if (signUpToCourseCommandResponse.Succeeded)
            {
                var updCourseStatisticsCommand = new UpdateCourseStatisticsCommand
                {
                    Course = signUpToCourseCommandResponse.Course,
                    Student = signUpToCourseCommandResponse.Student
                };

                var updateCourseStatisticsCommandResponse = await _updateCourseStatisticsHandler.Handle(updCourseStatisticsCommand);

                if (!updateCourseStatisticsCommandResponse.Succeeded)
                {
                    appResp.Message = "Oops, Something went wrong updating statistics.";
                    appResp.AddErrorsRange(updateCourseStatisticsCommandResponse.Errors);
                }
                else
                {
                    var email = new Email();
                    email.To = student.Email;
                    email.Body = "Contratulation, You have been enrolled to a new Course";
                    await _emailSender.SendEmailAsync(email);
                    appResp.Message = "OK - Success";
                    appResp.Response = true;
                }

                return appResp;
            }
            else
            {
                appResp.Message = "Sorry, Something went wrong";
                appResp.AddErrorsRange(signUpToCourseCommandResponse.Errors);
            }

            return appResp;

        }
    }
}
