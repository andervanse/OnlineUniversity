using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineUniversity.Application.DTOs;
using OnlineUniversity.Application.Interfaces;
using System.Net.Mime;
using System.Threading.Tasks;

namespace OnlineUniversity.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseApplication _courseApplication;

        public CoursesController(ICourseApplication courseApplication)
        {
            _courseApplication = courseApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var application = await _courseApplication.GetAllAsync();
            return Ok(application.Response);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> GetAsync(string id)
        {
            if (ModelState.IsValid)
            {
                var appResponse = await _courseApplication.GetByIdAsync(id);

                if (appResponse.Succeeded)
                {
                    return Ok(appResponse.Response);
                }
                else
                {
                    ModelState.AddModelError("Error", appResponse.Message);
                    ModelState.AddModelError("Details", string.Join(", ", appResponse.Errors));
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] CourseDto course)
        {
            if (ModelState.IsValid)
            {
                var resp = await _courseApplication.CreateAsync(course);

                if (resp.Succeeded)
                {
                    return CreatedAtRoute("GetById", new { id = course.Id.ToString() }, course);
                }
                else
                {
                    ModelState.AddModelError("Error", resp.Message);
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
