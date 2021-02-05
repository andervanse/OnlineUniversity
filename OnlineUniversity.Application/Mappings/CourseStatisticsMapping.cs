using AutoMapper;
using OnlineUniversity.Application.DTOs;
using OnlineUniversity.Domain;


namespace OnlineUniversity.Application.Mappings
{
    public class CourseStatisticsMapping : Profile
    {
        public CourseStatisticsMapping()
        {
            CreateMap<CourseStatistics, CourseStatisticsDto>();
        }
    }
}
