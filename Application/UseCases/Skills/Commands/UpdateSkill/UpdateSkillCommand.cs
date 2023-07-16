using AutoMapper;
using AutoMapper.Internal.Mappers;
using MediatR;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Skills.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineSkillMarketplace.Application.UseCases.Skills.Commands.UpdateSkill;


public class UpdateSkillCommand : IRequest<SkillDto>
{
    public Guid Id { get; set; }
    public string SkillName { get; set; }


}
public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, SkillDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public UpdateSkillCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<SkillDto> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        Skill skill =  FilterIfSkillExsists(request.Id);

        skill.SkillName=request.SkillName;

        _dbContext.Skills.Update(skill);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SkillDto>(skill);
    }

    private Skill FilterIfSkillExsists(Guid id)
    {
        Skill? skill =  _dbContext.Skills
            .Find(id);

        return skill
            ?? throw new NotFoundException(
                " there is no skill with this id. ");
    }
}
