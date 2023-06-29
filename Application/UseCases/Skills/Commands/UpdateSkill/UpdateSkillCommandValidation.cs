using FluentValidation;
using OnlineSkillMarketplace.Application.UseCases.Skills.Commands.UpdateSkill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineJobMarketplace.Application.UseCases.Skills.Commands.UpdateSkill
{
    public class UpdateSkillCommandValidation : AbstractValidator<UpdateSkillCommand>
    {
        public UpdateSkillCommandValidation()
        {
            RuleFor(x => x.SkillName)
                .MaximumLength(25)
                .WithMessage("Skill name can't be longer than 25 characters. ");
        }

    }
}
