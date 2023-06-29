using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Companies.Models;
using OnlineJobMarketplace.Application.UseCases.Students.Models;
using OnlineJobMarketplace.Domein.Entities;
using OnlineJobMarketplace.Domein.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineJobMarketplace.Application.UseCases.Skills.Commands.CreateSkill.CreateSkill;

public record CreateCompanyCommand : IRequest<CompanyDto>
{
    public string CompanyName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [RegularExpression(@"^\+998(33|9[0-9])\d{7}$", ErrorMessage = " Invalid PhoneNumber style. ")]
    public string PhoneNumber { get; set; }

    public string Password { get; set; }

    public string Location { get; set; }

    public string WebSite { get; set; }
}
public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public CreateCompanyCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {

        FilterIfCompanyExsists(request.CompanyName);

        Company company = _mapper.Map<Company>(request);

        await  _dbContext.Companies.AddAsync(company);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CompanyDto>(company);
    }

    private void FilterIfCompanyExsists(string? CompanyName)
    {
        Company? company = _dbContext.Companies
            .FirstOrDefault(x => x.CompanyName == CompanyName);

        if (company is not null)
        {
            throw new AlreadyExsistsException(
                " There is a  company with this name. CompanyName should be unique.  ");
        }
    }
}
