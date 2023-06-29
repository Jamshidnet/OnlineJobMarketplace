using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Jobs.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineJobMarketplace.Application.UseCases.Jobs.Queries.PaginatedJobQuery;

public record GetAllJobQuery(int PageNumber = 1, int PageSize = 10) : IRequest<List<JobDto>>;

public class GetallJobCommmandHandler : IRequestHandler<GetAllJobQuery, List<JobDto>>
{

    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetallJobCommmandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<JobDto>> Handle(GetAllJobQuery request, CancellationToken cancellationToken)
    {
        Job[] jobs = await _dbContext.Jobs.ToArrayAsync();

        List<JobDto> dtos = _mapper.Map<JobDto[]>(jobs).ToList();

        return dtos;
    }
}
