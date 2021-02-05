using System.Threading.Tasks;

namespace OnlineUniversity.Domain.Commands.Interfaces
{
    public interface IUpdateCourseStatisticsCommandHandler
    {
        Task<UpdateCourseStatisticsCommandResponse> Handle(UpdateCourseStatisticsCommand command);
    }
}
