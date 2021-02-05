using Microsoft.AspNetCore.Mvc;
using OnlineUniversity.Application.DTOs;
using OnlineUniversity.Application.Interfaces;
using System.Threading.Tasks;


namespace OnlineUniversity.WebAPI.Controllers
{
    [Route("api/course/sign-up")]
    [ApiController]
    public class SignUpToCourseController : ControllerBase
    {
        private readonly ISignUpToCourseApplication _signUpApplication;

        public SignUpToCourseController(ISignUpToCourseApplication signUpApplication)
        {
            _signUpApplication = signUpApplication;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SignUpToCourseDto model)
        {
            if (ModelState.IsValid)
            {
                var appResponse = await _signUpApplication.SignUpStudentToCourseAsync(model.CourseId, model.Student);

                if (appResponse.Succeeded)
                {
                    return Ok(appResponse.Message);
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

    }
}
