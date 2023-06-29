using LazyCache;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using OnlineJobMarketplace.Application.UseCases.Skills.Models;
using OnlineSkillMarketplace.Application.UseCases.Skills.Commands.CreateSkill;
using OnlineSkillMarketplace.Application.UseCases.Skills.Quiries.GetSkill;
using OnlineSkillMarketplace.Application.UseCases.Skills.Quiries.PaginatedSkill;
using OnlineSkillMarketplace.Application.UseCases.Skills.Commands.UpdateSkill;
using OnlineSkillMarketplace.Application.UseCases.Skills.Commands.DeleteSkill;

namespace OnlineJobMarketplace.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ApiBaseController
    {

        public SkillController(IAppCache appCache, IConfiguration configuration)
        {
            _appCache = appCache;
            _configuration = configuration;
        }

        [HttpPost]
        public async ValueTask<ActionResult<SkillDto>> CreateSkillAsync(CreateSkillCommand command)
            => Ok(await Mediator.Send(command));


        [HttpGet("{skillId}")]
        public async ValueTask<ActionResult<SkillDto>> GetSkillAsync(Guid skillId)
                 => await Mediator.Send(new GetSkillQuery(skillId));

        [HttpGet]
        public async ValueTask<ActionResult<List<SkillDto>>> GetSkillsWithPaginated(int page = 1)
        {
            IPagedList<SkillDto> query = (await Mediator
                   .Send(new GetAllSkillQuery()))
                   .ToPagedList(page, 10);
            return Ok(query);
        }

        [HttpPut]
        public async ValueTask<ActionResult<SkillDto>> PutSkillAsync(UpdateSkillCommand command)
                => await Mediator.Send(command);

        [HttpDelete("{skillId}")]
        public async ValueTask<ActionResult<SkillDto>> DeleteSkillAsync(Guid skillId)
                => await Mediator.Send(new DeleteSkillCommand(skillId));

    }
}
