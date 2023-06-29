using AutoMapper;
using MediatR;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Skills.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineSkillMarketplace.Application.UseCases.Skills.Commands.CreateSkill
{

    public class CreateSkillCommand : IRequest<SkillDto>
    {
        public string SkillName { get; set; }

    }
    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, SkillDto>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public CreateSkillCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SkillDto> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {

            FilterIfSkillExsists(request.SkillName);

            Skill skill = _mapper.Map<Skill>(request);

            await _dbContext.Skills.AddAsync(skill);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SkillDto>(skill);
        }

        private void FilterIfSkillExsists(string skillName)
        {
            Skill? skill = _dbContext.Skills
                .FirstOrDefault(x => x.SkillName == skillName);

            if (skill is not null)
            {
                throw new AlreadyExsistsException(
                    "There is a skill with this name. ");
            }
        }
    }

}
