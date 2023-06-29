using FluentValidation;
using OnlineJobMarketplace.Application.UseCases.Skills.Commands.CreateSkill.CreateSkill;

namespace OnlineJobMarketplace.Application.UseCases.Students.Commands.CreateStudent;

public class CreateCompanyCommandValidation : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidation()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage("Company Name is required . ");
    }

}
