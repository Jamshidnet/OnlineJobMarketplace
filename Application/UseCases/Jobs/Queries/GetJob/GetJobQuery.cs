using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Jobs.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineJobMarketplace.Application.UseCases.Jobs.Queries.GetJob;

public record GetJobQuery(Guid Id) : IRequest<JobWithRequiredSkillsDto>;

public class GetJobQueryHandler : IRequestHandler<GetJobQuery, JobWithRequiredSkillsDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetJobQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<JobWithRequiredSkillsDto> Handle(GetJobQuery request, CancellationToken cancellationToken)
    {
        Job candidate = FilterIfJobExsists(request.Id);

        return _mapper.Map<JobWithRequiredSkillsDto>(candidate);
    }

    private Job FilterIfJobExsists(Guid id)
    {
        return _dbContext.Jobs
            .Include(x => x.RequiredSkills)
            .FirstOrDefault(x => x.Id == id)
            ?? throw new NotFoundException(
                " There is no candidate with this Id. ");

    }


}