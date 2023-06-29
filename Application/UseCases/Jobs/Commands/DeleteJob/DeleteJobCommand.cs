using AutoMapper;
using MediatR;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Jobs.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineJobMarketplace.Application.UseCases.Jobs.Commands.DeleteJob;

public record DeleteJobCommand(Guid Id) : IRequest<JobDto>;

public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, JobDto>
{
    private IApplicationDbContext _dbContext;
    private IMapper _mapper;

    public DeleteJobCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<JobDto> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        Job job = FilterIfJobExsists(request.Id);

        _dbContext.Jobs.Remove(job);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<JobDto>(job);
    }

    private Job FilterIfJobExsists(Guid id)
             => _dbContext.Jobs.Find(id)
                ?? throw new NotFoundException(
                   " There is no job with id. ");
    
}


