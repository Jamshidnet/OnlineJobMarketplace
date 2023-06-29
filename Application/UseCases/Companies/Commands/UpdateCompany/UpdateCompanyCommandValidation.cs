using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineJobMarketplace.Application.UseCases.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommandValidation : AbstractValidator<UpdateCompanyCommand>
    {
        
        public UpdateCompanyCommandValidation()
        {
            RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage("Company Name is required . ");
        }
    }
}
