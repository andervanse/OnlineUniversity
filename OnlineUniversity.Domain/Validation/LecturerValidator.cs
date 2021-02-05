using FluentValidation;

namespace OnlineUniversity.Domain.Validation
{
    public class LecturerValidator : AbstractValidator<Lecturer>
    {
        public LecturerValidator()
        {
            RuleFor(l => l.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(200);
        }
    }
}
