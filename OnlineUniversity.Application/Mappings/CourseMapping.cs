using AutoMapper;
using OnlineUniversity.Application.DTOs;
using OnlineUniversity.Domain;

namespace OnlineUniversity.Application.Mappings
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<Course, CourseDto>().ForMember(dest => dest.Lecturer, opt => opt.MapFrom(src => src.Lecturer)).ReverseMap();
        }
    }
}
