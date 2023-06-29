using AutoMapper;
using MediatR;
using OnlineJobMarketplace.Domein.Entities;
using OnlineJobMarketplace.Application.UseCases.Jobs.Models;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.Common.Exceptions;

namespace JobPaymentSystem.Application.UseCases.Jobs.Commands.CreateJob;

public class CreateJobCommand : IRequest<JobWithRequiredSkillsDto>
{
    public Guid CompanyId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Requirments { get; set; }

    public ICollection<Guid> RequiredSkills { get; set; }

}
public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, JobWithRequiredSkillsDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public CreateJobCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<JobWithRequiredSkillsDto> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {

        FilterIfJobExsists(request.CompanyId, request.Title);
        ICollection<Skill>? skills = FilterIfAllSkillsExsist(request.RequiredSkills);

        Job job = _mapper.Map<Job>(request);
        job.RequiredSkills = skills;
        

        await  _dbContext.Jobs.AddAsync(job);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<JobWithRequiredSkillsDto>(job);
    }

    private ICollection<Skill> FilterIfAllSkillsExsist(ICollection<Guid> skills)
    {
        List<Skill> maybeSkills = new();
        foreach (Guid Id in skills)
        {
            var skill = _dbContext.Skills.FirstOrDefault(c => c.Id == Id)
                ?? throw new NotFoundException($" There is no skill with this {Id} id. ");
            maybeSkills.Add(skill);
        }

        return maybeSkills;
    }

    private void FilterIfJobExsists(Guid companyId, string? title)
    {
        Job? job = _dbContext.Jobs
            .FirstOrDefault(x => x.CompanyId == companyId
            && x.Title==title);

        if (job is not null)
        {
            throw new AlreadyExsistsException(
                @" This company has already hired a job with this title.
                    In order to hire this again, delete the exsisting vacation and try again. 
                    An overall, a company can hire jobs with uniqe title names. ");
        }
    }
}
