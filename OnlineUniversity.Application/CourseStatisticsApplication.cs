
using AutoMapper;
using OnlineUniversity.Application.DTOs;
using OnlineUniversity.Domain.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineUniversity.Application
{
    public class CourseStatisticsApplication : ICourseStatisticsApplication
    {
        private readonly ICourseStatisticsRepository _courseStatisticsRepository;
        private readonly IMapper _mapper;

        public CourseStatisticsApplication(
            ICourseStatisticsRepository courseStatisticsRepository,
            IMapper mapper)
        {
            _courseStatisticsRepository = courseStatisticsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseStatisticsDto>> GetAllAsync()
        {
            var domainList = await _courseStatisticsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseStatisticsDto>>(domainList);
        }
    }
}
