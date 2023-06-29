using FluentValidation;
using OnlineJobMarketplace.Application.UseCases.Skills.Commands.CreateSkill.CreateSkill;

namespace OnlineJobMarketplace.Application.UseCases.Students.Commands.CreateStudent;

public class CreateCandidateCommandValidation : AbstractValidator<CreateCandidateCommand>
{
    public CreateCandidateCommandValidation()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(" Name is required . ")
            .MaximumLength(20)
            .WithMessage("First Name should be less than 20 characters. ");
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last Name is required . ")
            .MaximumLength(20)
            .WithMessage("Last Name should be less than 20 characters. ");

        RuleFor(x => x.ExperienceDuration)
            .GreaterThanOrEqualTo(0)
            .WithMessage(" Experience year should be more than 0. ");

        RuleFor(x => x.Birthdate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .GreaterThanOrEqualTo(new DateTime(1800, 01, 01))
            .WithMessage("Invalid Interval for Birthdate. ");

        RuleFor(x => x.Skills.Count)
        .GreaterThanOrEqualTo(3)
            .WithMessage(" Candidate should have at least 3 skills. ");

    }

}
