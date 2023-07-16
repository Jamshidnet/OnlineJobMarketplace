using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Jobs.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineJobMarketplace.Application.UseCases.Jobs.Commands.UpdateJob;

public class UpdateJobCommand : IRequest<JobWithRequiredSkillsDto>
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Requirments { get; set; }

    public ICollection<Guid> RequiredSkills { get; set; }

}
public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, JobWithRequiredSkillsDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public UpdateJobCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<JobWithRequiredSkillsDto> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        Job job = await FilterIfJobExsists(request.Id);
        IEnumerable<Skill> skills = FilterifSkillIdsAreAvialible(request.RequiredSkills);

        _mapper.Map(request, job);
        job.RequiredSkills = skills.ToList();

        _dbContext.Jobs.Update(job);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<JobWithRequiredSkillsDto>(job);
    }

    private IEnumerable<Skill> FilterifSkillIdsAreAvialible(ICollection<Guid> jobIds)
    {
        foreach (var Id in jobIds)
            yield return _dbContext.Skills.Find(Id)
                ?? throw new NotFoundException($" there is no skill with this {Id} id. ");
    }

    private async Task<Job> FilterIfJobExsists(Guid id)
    {
        Job? job = await _dbContext.Jobs
            .FirstOrDefaultAsync(x => x.Id == id);

        return job
            ?? throw new NotFoundException(
                " there is no job with this id. ");
    }
}
