using FluentValidation;

namespace OnlineJobMarketplace.Application.UseCases.Jobs.Commands.UpdateJob
{
    public  class UpdateJobCommandValidation : AbstractValidator<UpdateJobCommand>
    {
        public UpdateJobCommandValidation() 
        {
            RuleFor(x => x.CompanyId)
                        .NotEmpty()
                        .WithMessage(" Company is required . ");

            RuleFor(x => x.Title)
                .MaximumLength(50)
                .WithMessage(" Title should consist of at most 50 letters. ");

            RuleFor(x => x.Description)
                .MinimumLength(50)
                .WithMessage(" Description should consists of at least 50 letters. ");

            RuleFor(x => x.Requirments)
                .MinimumLength(50)
                .WithMessage(" Requirements should consists of at least 50 letters. ");

            RuleFor(x => x.RequiredSkills.Count)
            .GreaterThanOrEqualTo(1)
                .WithMessage(" Job should consists of at least 1 skill. ");

        }


    }
}
