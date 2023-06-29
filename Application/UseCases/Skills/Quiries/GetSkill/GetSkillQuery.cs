using AutoMapper;
using MediatR;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Skills.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineSkillMarketplace.Application.UseCases.Skills.Quiries.GetSkill;


public record GetSkillQuery(Guid Id) : IRequest<SkillDto>;

public class GetSkillQueryHandler : IRequestHandler<GetSkillQuery, SkillDto>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetSkillQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<SkillDto> Handle(GetSkillQuery request, CancellationToken cancellationToken)
    {
        Skill candidate = FilterIfSkillExsists(request.Id);

        return _mapper.Map<SkillDto>(candidate);
    }

    private Skill FilterIfSkillExsists(Guid id)
    {
        return _dbContext.Skills.Find(id)
            ?? throw new NotFoundException(
                " There is no candidate with this Id. ");

    }



}