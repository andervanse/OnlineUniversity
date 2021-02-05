using FluentValidation;

namespace OnlineUniversity.Domain.Validation
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(200);

            RuleFor(s => s.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(s => s.DateOfBirth)
                .NotNull();
        }
    }
}
