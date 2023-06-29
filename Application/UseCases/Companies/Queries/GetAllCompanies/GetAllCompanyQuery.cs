using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Companies.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineJobMarketplace.Application.UseCases.Companies.Queries.GetAllCompanies;

public record GetAllCompanyQuery(int PageNumber = 1, int PageSize = 10) : IRequest<List<CompanyDto>>;

public class GetallCompanyCommmandHandler : IRequestHandler<GetAllCompanyQuery, List<CompanyDto>>
{

    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetallCompanyCommmandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<CompanyDto>> Handle(GetAllCompanyQuery request, CancellationToken cancellationToken)
    {
        Company[] companys = await _dbContext.Companies.ToArrayAsync();

        List<CompanyDto> dtos = _mapper.Map<CompanyDto[]>(companys).ToList();

        return dtos;
    }
}
