using OnlineJobMarketplace.Application.UseCases.Skills.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineJobMarketplace.Application.UseCases.Students.Models;

public record CandidateDtoWithSkills : CandidateDto
{
    public ICollection<SkillDto> Skills { get; set; }
}
