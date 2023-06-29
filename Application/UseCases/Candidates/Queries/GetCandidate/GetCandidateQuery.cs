using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Domein.Entities;
using OnlineJobMarketplace.Application.UseCases.Students.Models;

namespace OnlineJobMarketplace.Application.UseCases.Candidates.Queries.GetCandidate;

public  record GetCandidateQuery(Guid Id) : IRequest<CandidateDtoWithSkills>;

public class GetCandidateQueryHandler : IRequestHandler<GetCandidateQuery, CandidateDtoWithSkills>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetCandidateQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<CandidateDtoWithSkills> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
    {
        Candidate candidate = FilterIfCandidateExsists(request.Id);

        return _mapper.Map<CandidateDtoWithSkills>(candidate);
    }

    private Candidate FilterIfCandidateExsists(Guid id)
    {
        Candidate? candidate = _dbContext.Candidates
            .Include(x=>x.Skills)
            .FirstOrDefault(x => x.Id == id)
            ?? throw new NotFoundException(
                " There is no candidate with this Id. ");

        return candidate;
    }


}