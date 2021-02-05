
using AutoMapper;
using OnlineUniversity.Application.DTOs;
using OnlineUniversity.Application.Interfaces;
using OnlineUniversity.Domain;
using OnlineUniversity.Domain.Repository;
using System;
using System.Threading.Tasks;

namespace OnlineUniversity.Application
{
    public class LecturerApplication : ILecturerApplication
    {
        private readonly ILecturerRepository _lecturerRepository;
        private readonly IMapper _mapper;

        public LecturerApplication(
            IMapper mapper,
            ILecturerRepository lecturerRepository)
        {
            _lecturerRepository = lecturerRepository;
            _mapper = mapper;
        }



        public async Task<ApplicationResponse<bool>> CreateAsync(LecturerDto lecturer)
        {
            Lecturer domainObj = _mapper.Map<Lecturer>(lecturer);
            bool created = await _lecturerRepository.CreateAsync(domainObj);
            lecturer.Id = domainObj.Id;

            if (created)
            {
                return new ApplicationResponse<bool> { Message = "OK" };
            }
            else
            {
                var response = new ApplicationResponse<bool>();
                response.AddError("Sorry, Something went wrong");
                return response;
            }
        }

        public async Task<ApplicationResponse<LecturerDto>> GetByIdAsync(string lecturerId)
        {
            Guid lecturerGuid = new Guid();

            if (Guid.TryParse(lecturerId, out lecturerGuid))
            {
                Lecturer lecturer = await _lecturerRepository.GetByIdAsync(lecturerGuid);
                var appResp = new ApplicationResponse<LecturerDto>();

                if (lecturer != null)
                {
                    LecturerDto dto = _mapper.Map<LecturerDto>(lecturer);
                    appResp.Response = dto;
                    appResp.Message = "OK";
                    return appResp;
                }
                else
                {
                    appResp.AddError("Not Found");
                    return appResp;
                }
            }
            else
            {
                var response = new ApplicationResponse<LecturerDto>();
                response.Response = new LecturerDto();
                response.AddError("Identifier is in a wrong format.");
                return response;
            }
        }
    }
}
