using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Companies.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineJobMarketplace.Application.UseCases.Companies.Queries.GetCompany;

public record GetCompanyQuery(Guid Id) : IRequest<CompanyWithJobsDto>;

public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyWithJobsDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetCompanyQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<CompanyWithJobsDto> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        Company company = FilterIfCompanyExsists(request.Id);

        return _mapper.Map<CompanyWithJobsDto>(company);
    }

    private Company FilterIfCompanyExsists(Guid id)
    {
        Company? company = _dbContext.Companies
            .Include(x => x.Jobs)
            .FirstOrDefault(x => x.Id == id)
            ?? throw new NotFoundException(
                " There is no company with this Id. ");

        return company;
    }


}
