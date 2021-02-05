using System.Threading.Tasks;

namespace OnlineUniversity.Domain.Commands.Interfaces
{
    public interface ISignUpToCourseCommandHandler
    {
        Task<SignUpToCourseCommandResponse> Handle(SignUpToCourseCommand command);
    }
}
