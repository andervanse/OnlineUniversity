using AutoMapper;
using OnlineUniversity.Application.DTOs;
using OnlineUniversity.Domain;
using OnlineUniversity.Domain.Commands;

namespace OnlineUniversity.Application.Mappings
{
    public class StudentMapping : Profile
    {
        public StudentMapping()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<StudentDto, SignUpToCourseCommand>();
        }
    }
}
