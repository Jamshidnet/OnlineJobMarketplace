using LazyCache;
using Microsoft.AspNetCore.Mvc;
using OnlineJobMarketplace.Application.UseCases.Candidates.Commands.DeleteCandidate;
using OnlineJobMarketplace.Application.UseCases.Candidates.Commands.UpdateCandidate;
using OnlineJobMarketplace.Application.UseCases.Candidates.Queries.GetCandidate;
using OnlineJobMarketplace.Application.UseCases.Candidates.Queries.PaginatedCandidateQuery;
using OnlineJobMarketplace.Application.UseCases.Skills.Commands.CreateSkill.CreateSkill;
using OnlineJobMarketplace.Application.UseCases.Students.Models;
using PagedList;

namespace OnlineJobMarketplace.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CandidateController : ApiBaseController
{
    public CandidateController(IAppCache appCache, IConfiguration configuration)
    {
        _appCache = appCache;
        _configuration = configuration;
    }

    [HttpPost]
    public async ValueTask<ActionResult<CandidateDtoWithSkills>> CreateCandidateAsync(CreateCandidateCommand command)
        => Ok(await Mediator.Send(command));


    [HttpGet("{candidateId}")]
    public async ValueTask<ActionResult<CandidateDtoWithSkills>> GetCandidateAsync(Guid candidateId)
             =>    await Mediator.Send(new GetCandidateQuery(candidateId));

    [HttpGet]
    public async ValueTask<ActionResult<List<CandidateDto>>> GetCandidatesWithPaginated(int page =1)
    {
        IPagedList<CandidateDto> query = (await Mediator
               .Send(new GetAllCandidateQuery()))
               .ToPagedList(page, 10);
                return Ok(query);
    }

    [HttpPut]
    public async ValueTask<ActionResult<CandidateDtoWithSkills>> PutCandidateAsync(UpdateCandidateCommand command)
            =>  await Mediator.Send(command);

    [HttpDelete("{candidateId}")]
    public async ValueTask<ActionResult<CandidateDto>> DeleteCandidateAsync(Guid candidateId)
            =>  await Mediator.Send(new DeleteCandidateCommand(candidateId));

}
