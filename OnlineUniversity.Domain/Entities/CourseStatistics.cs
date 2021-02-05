
using System;

namespace OnlineUniversity.Domain
{
    public class CourseStatistics
    {
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public decimal AverageAge { get; set; }
        public int SumAges { get; set; }
    }
}
