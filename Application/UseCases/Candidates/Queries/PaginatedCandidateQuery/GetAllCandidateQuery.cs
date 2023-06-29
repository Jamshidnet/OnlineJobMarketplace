using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Students.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineJobMarketplace.Application.UseCases.Candidates.Queries.PaginatedCandidateQuery;

public record GetAllCandidateQuery(int PageNumber = 1, int PageSize = 10) : IRequest<List<CandidateDto>>;

public class GetallCandidateCommmandHandler : IRequestHandler<GetAllCandidateQuery, List<CandidateDto>>
{

    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetallCandidateCommmandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<CandidateDto>> Handle(GetAllCandidateQuery request, CancellationToken cancellationToken)
    {
        Candidate[] candidates = await _dbContext.Candidates.ToArrayAsync();

        List<CandidateDto> dtos = _mapper.Map<CandidateDto[]>(candidates).ToList();

        return dtos;
    }
}
