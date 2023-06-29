using AutoMapper;
using MediatR;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Students.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineJobMarketplace.Application.UseCases.Candidates.Commands.DeleteCandidate;

public record DeleteCandidateCommand(Guid Id) : IRequest<CandidateDto>;

public class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, CandidateDto>
{
    private IApplicationDbContext _dbContext;
    private IMapper _mapper;

    public DeleteCandidateCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CandidateDto> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
    {
        Candidate candidate = FilterIfCandidateExsists(request.Id);

        _dbContext.Candidates.Remove(candidate);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CandidateDto>(candidate);
    }

    private Candidate FilterIfCandidateExsists(Guid id)
    {
        Candidate? candidate = _dbContext.Candidates.FirstOrDefault(c => c.Id == id);

        if (candidate is null)
        {
            throw new NotFoundException(" There is no candidate with id. ");
        }

        return candidate;
    }
}


