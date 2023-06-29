using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Submissions.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineJobMarketplace.Application.UseCases.Submissions.Queries.PaginatedSubmission;


public record GetAllSubmissionQuery(int PageNumber = 1, int PageSize = 10) : IRequest<List<SubmissionDto>>;

public class GetallSubmissionCommmandHandler : IRequestHandler<GetAllSubmissionQuery, List<SubmissionDto>>
{

    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetallSubmissionCommmandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<SubmissionDto>> Handle(GetAllSubmissionQuery request, CancellationToken cancellationToken)
    {
        Submission[] submissions = await _dbContext.Submissions.ToArrayAsync();

        List<SubmissionDto> dtos = _mapper.Map<SubmissionDto[]>(submissions).ToList();

        return dtos;
    }
}
