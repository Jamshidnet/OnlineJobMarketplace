using AutoMapper;
using MediatR;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Skills.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineSkillMarketplace.Application.UseCases.Skills.Commands.DeleteSkill
{

    public record DeleteSkillCommand(Guid Id) : IRequest<SkillDto>;

    public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, SkillDto>
    {
        private IApplicationDbContext _dbContext;
        private IMapper _mapper;

        public DeleteSkillCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SkillDto> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            Skill skill = FilterIfSkillExsists(request.Id);

            _dbContext.Skills.Remove(skill);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SkillDto>(skill);
        }

        private Skill FilterIfSkillExsists(Guid id)
                 => _dbContext.Skills.Find(id)
                    ?? throw new NotFoundException(
                       " There is no skill with id. ");

    }



}
