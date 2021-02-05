using OnlineUniversity.Application.DTOs;
using System;
using System.Threading.Tasks;

namespace OnlineUniversity.Application.Interfaces
{
    public interface ISignUpToCourseApplication
    {
        Task<ApplicationResponse<bool>> SignUpStudentToCourseAsync(Guid courseId, StudentDto student);
    }
}
