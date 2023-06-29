using JobPaymentSystem.Application.UseCases.Jobs.Commands.CreateJob;
using LazyCache;
using Microsoft.AspNetCore.Mvc;
using OnlineJobMarketplace.Application.UseCases.Jobs.Commands.DeleteJob;
using OnlineJobMarketplace.Application.UseCases.Jobs.Commands.UpdateJob;
using OnlineJobMarketplace.Application.UseCases.Jobs.Models;
using OnlineJobMarketplace.Application.UseCases.Jobs.Queries.GetJob;
using OnlineJobMarketplace.Application.UseCases.Jobs.Queries.PaginatedJobQuery;
using PagedList;

namespace OnlineJobMarketplace.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ApiBaseController
    {
        public JobController(IAppCache appCache, IConfiguration configuration)
        {
            _appCache = appCache;
            _configuration = configuration;
        }

        [HttpPost]
        public async ValueTask<ActionResult<JobWithRequiredSkillsDto>> CreateJobAsync(CreateJobCommand command)
            => Ok(await Mediator.Send(command));


        [HttpGet("{jobId}")]
        public async ValueTask<ActionResult<JobWithRequiredSkillsDto>> GetJobAsync(Guid jobId)
                 => await Mediator.Send(new GetJobQuery(jobId));

        [HttpGet]
        public async ValueTask<ActionResult<List<JobDto>>> GetJobsWithPaginated(int page = 1)
        {
            IPagedList<JobDto> query = (await Mediator
                   .Send(new GetAllJobQuery()))
                   .ToPagedList(page, 10);
            return Ok(query);
        }

        [HttpPut]
        public async ValueTask<ActionResult<JobWithRequiredSkillsDto>> PutJobAsync(UpdateJobCommand command)
                => await Mediator.Send(command);

        [HttpDelete("{jobId}")]
        public async ValueTask<ActionResult<JobDto>> DeleteJobAsync(Guid jobId)
                => await Mediator.Send(new DeleteJobCommand(jobId));

    }
}
