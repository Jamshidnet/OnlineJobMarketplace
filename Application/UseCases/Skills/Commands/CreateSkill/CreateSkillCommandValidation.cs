using FluentValidation;
using OnlineSkillMarketplace.Application.UseCases.Skills.Commands.CreateSkill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineJobMarketplace.Application.UseCases.Skills.Commands.CreateSkill
{
    public  class CreateSkillCommandValidation : AbstractValidator<CreateSkillCommand>
    {
        public CreateSkillCommandValidation()
        {
            RuleFor(x => x.SkillName)
                .MaximumLength(25)
                .WithMessage("Skill name can't be longer than 25 characters. ");
        }

    }
}
