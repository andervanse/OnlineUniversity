using AutoMapper;
using OnlineUniversity.Application.DTOs;
using OnlineUniversity.Domain;

namespace OnlineUniversity.Application.Mappings
{
    public class LecturerMapping : Profile
    {
        public LecturerMapping()
        {
            CreateMap<Lecturer, LecturerDto>().ReverseMap();
        }
    }
}
