using OnlineJobMarketplace.Application.UseCases.Skills.Models;

namespace OnlineJobMarketplace.Application.UseCases.Jobs.Models;

public class JobWithRequiredSkillsDto : JobDto
{
    public ICollection<SkillDto> RequiredSkills { get; set; }
}
