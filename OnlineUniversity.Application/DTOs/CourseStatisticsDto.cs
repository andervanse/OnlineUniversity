using System;


namespace OnlineUniversity.Application.DTOs
{
    public class CourseStatisticsDto
    {
        public string Name { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public decimal AverageAge { get; set; }
    }
}
