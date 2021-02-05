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
    public class LecturersController : ControllerBase
    {
        private readonly ILecturerApplication _lecturerApplication;

        public LecturersController(ILecturerApplication LecturerApplication)
        {
            _lecturerApplication = LecturerApplication;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            if (ModelState.IsValid)
            {
                var appResponse = await _lecturerApplication.GetByIdAsync(id);

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
        public async Task<IActionResult> Post([FromBody] LecturerDto lecturer)
        {
            if (ModelState.IsValid)
            {
                var resp = await _lecturerApplication.CreateAsync(lecturer);

                if (resp.Succeeded)
                {
                    return CreatedAtRoute(nameof(GetAsync), new { id = lecturer.Id }, lecturer);
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
