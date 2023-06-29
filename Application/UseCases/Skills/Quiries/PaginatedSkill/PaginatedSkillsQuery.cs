using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Skills.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineSkillMarketplace.Application.UseCases.Skills.Quiries.PaginatedSkill;


public record GetAllSkillQuery(int PageNumber = 1, int PageSize = 10) : IRequest<List<SkillDto>>;

public class GetallSkillCommmandHandler : IRequestHandler<GetAllSkillQuery, List<SkillDto>>
{

    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public GetallSkillCommmandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<SkillDto>> Handle(GetAllSkillQuery request, CancellationToken cancellationToken)
    {
        Skill[] skills = await _dbContext.Skills.ToArrayAsync();

        List<SkillDto> dtos = _mapper.Map<SkillDto[]>(skills).ToList();

        return dtos;
    }
}
