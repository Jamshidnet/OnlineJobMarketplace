using AutoMapper;
using MediatR;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Submissions.Models;
using OnlineJobMarketplace.Domein.Entities;
using OnlineJobMarketplace.Domein.Enums;

namespace OnlineJobMarketplace.Application.UseCases.Submissions.Commands.UpdateSubmission;


public class UpdateSubmissionCommand : IRequest<SubmissionDto>
{
    public Guid Id { get; set; }
    public Guid JobId { get; set; }

    public Guid CandidateId { get; set; }

    public SubmissionStatus Status { get; set; }

}
public class UpdateSubmissionCommandHandler : IRequestHandler<UpdateSubmissionCommand, SubmissionDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public UpdateSubmissionCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<SubmissionDto> Handle(UpdateSubmissionCommand request, CancellationToken cancellationToken)
    {
        Submission submission = await FilterIfSubmissionExsists(request.Id);

        submission = _mapper.Map<Submission>(request);

        _dbContext.Submissions.Update(submission);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SubmissionDto>(submission);
    }

    private async Task<Submission> FilterIfSubmissionExsists(Guid id)
    {
        Submission? submission = await _dbContext.Submissions
            .FindAsync(id);

        return submission
            ?? throw new NotFoundException(
                " there is no submission with this id. ");
    }
}
