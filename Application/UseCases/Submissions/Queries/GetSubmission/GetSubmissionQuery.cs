using AutoMapper;
using MediatR;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Submissions.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineJobMarketplace.Application.UseCases.Submissions.Queries.GetSubmission;


public record GetSubmissionQuery(Guid Id) : IRequest<SubmissionDto>;

public class GetSubmissionQueryHandler : IRequestHandler<GetSubmissionQuery, SubmissionDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetSubmissionQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<SubmissionDto> Handle(GetSubmissionQuery request, CancellationToken cancellationToken)
    {
        Submission submission = FilterIfSubmissionExsists(request.Id);

        return _mapper.Map<SubmissionDto>(submission);
    }

    private Submission FilterIfSubmissionExsists(Guid id)
    {
        return _dbContext.Submissions.Find(id)
            ?? throw new NotFoundException(
                " There is no candidate with this Id. ");

    }




}