using AutoMapper;
using MediatR;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Submissions.Models;
using OnlineJobMarketplace.Domein.Entities;
using OnlineJobMarketplace.Domein.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineJobMarketplace.Application.UseCases.Submissions.Commands.CreateSubmission;


public class CreateSubmissionCommand : IRequest<SubmissionDto>
{
    public Guid JobId { get; set; }

    public Guid CandidateId { get; set; }

    public SubmissionStatus Status { get; set; }

}
public class CreateSubmissionCommandHandler : IRequestHandler<CreateSubmissionCommand, SubmissionDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public CreateSubmissionCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<SubmissionDto> Handle(CreateSubmissionCommand request, CancellationToken cancellationToken)
    {

        FilterIfSubmissionExsists(request.JobId, request.CandidateId);

        Submission submission = _mapper.Map<Submission>(request);

        await _dbContext.Submissions.AddAsync(submission);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SubmissionDto>(submission);
    }

    private void FilterIfSubmissionExsists(Guid jobId, Guid candidateId)
    {
        Submission? submission = _dbContext.Submissions
            .FirstOrDefault(x => x.CandidateId == candidateId
            && x.JobId==jobId);

        if (submission is not null)
        {
            throw new AlreadyExsistsException(
                "This candidate has already applied for this job. ");
        }
    }
}
