using AutoMapper;
using MediatR;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Submissions.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineSubmissionMarketplace.Application.UseCases.Submissions.Commands.DeleteSubmission;


public record DeleteSubmissionCommand(Guid Id) : IRequest<SubmissionDto>;

public class DeleteSubmissionCommandHandler : IRequestHandler<DeleteSubmissionCommand, SubmissionDto>
{
    private IApplicationDbContext _dbContext;
    private IMapper _mapper;

    public DeleteSubmissionCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<SubmissionDto> Handle(DeleteSubmissionCommand request, CancellationToken cancellationToken)
    {
        Submission submission = FilterIfSubmissionExsists(request.Id);

        _dbContext.Submissions.Remove(submission);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SubmissionDto>(submission);
    }

    private Submission FilterIfSubmissionExsists(Guid id)
             => _dbContext.Submissions.Find(id)
                ?? throw new NotFoundException(
                   " There is no submission with id. ");

}
