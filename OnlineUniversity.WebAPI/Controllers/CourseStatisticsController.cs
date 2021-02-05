using Microsoft.AspNetCore.Mvc;
using OnlineUniversity.Application;
using System.Threading.Tasks;

namespace OnlineUniversity.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseStatisticsController : ControllerBase
    {
        private readonly ICourseStatisticsApplication _courseStatisticsApplication;

        public CourseStatisticsController(ICourseStatisticsApplication courseStatisticsApplication)
        {
            _courseStatisticsApplication = courseStatisticsApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var list = await _courseStatisticsApplication.GetAllAsync();
            return Ok(list);
        }
    }
}
