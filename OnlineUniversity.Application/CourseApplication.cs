using AutoMapper;
using OnlineUniversity.Application.DTOs;
using OnlineUniversity.Application.Interfaces;
using OnlineUniversity.Domain;
using OnlineUniversity.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineUniversity.Application
{
    public class CourseApplication : ICourseApplication
    {
        private readonly ICourseRepository _courseRepository;        
        private readonly IMapper _mapper;

        public CourseApplication(
            IMapper mapper,
            ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<ApplicationResponse<bool>> CreateAsync(CourseDto course)
        {
            Course domainObj = _mapper.Map<Course>(course);
            bool created = await _courseRepository.CreateAsync(domainObj);
            course.Id = domainObj.Id;
            course.Lecturer.Id = domainObj.Lecturer.Id;

            if (created)
            {
                return new ApplicationResponse<bool> { Message = "OK" };
            }
            else
            {
                var response = new ApplicationResponse<bool>();
                response.AddError("Sorry, Something went wrong");
                return response;
            }
        }

        public async Task<ApplicationResponse<CourseDto>> GetByIdAsync(string courseId)
        {
            Guid courseGuid = new Guid();

            if (Guid.TryParse(courseId, out courseGuid))
            {
                Course course = await _courseRepository.GetByIdAsync(courseGuid);

                var appResp = new ApplicationResponse<CourseDto>();

                if (course != null)
                {
                    CourseDto dto = _mapper.Map<CourseDto>(course);
                    appResp.Response = dto;
                    appResp.Message = "OK";
                    return appResp;
                }
                else
                {
                    appResp.Message = "Not Found";
                    appResp.AddError($"Course Id '{courseId}' Not Found.");
                    return appResp;
                }
            }
            else
            {
                var response = new ApplicationResponse<CourseDto>();
                response.Response = new CourseDto();
                response.AddError("Identifier is in a wrong format.");
                return response;
            }
        }


        public async Task<ApplicationResponse<IEnumerable<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(courses);

            var appResp = new ApplicationResponse<IEnumerable<CourseDto>>
            {
                Response = coursesDto
            };

            return appResp;
        }
    }
}
