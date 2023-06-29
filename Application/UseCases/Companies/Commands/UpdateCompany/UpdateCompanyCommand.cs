using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Companies.Models;
using OnlineJobMarketplace.Application.UseCases.Students.Models;
using OnlineJobMarketplace.Domein.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnlineJobMarketplace.Application.UseCases.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommand : IRequest<CompanyDto>
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [RegularExpression(@"^\+998(33|9[0-9])\d{7}$",
        ErrorMessage = " Invalid PhoneNumber style. ")]
    public string PhoneNumber { get; set; }

    public string Password { get; set; }

    public string Location { get; set; }

    public string WebSite { get; set; }
}

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public UpdateCompanyCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        Company company = await FilterIfCompanyExsists(request.Id);

        company = _mapper.Map<Company>(request);

        _dbContext.Companies.Update(company);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CompanyDto>(company);
    }

    private async Task<Company> FilterIfCompanyExsists(Guid id)
        => await _dbContext.Companies
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(
                " there is no company with this id. ");

}
