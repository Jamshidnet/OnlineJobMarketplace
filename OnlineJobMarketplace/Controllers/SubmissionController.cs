using LazyCache;
using Microsoft.AspNetCore.Mvc;
using OnlineJobMarketplace.Application.UseCases.Submissions.Commands.UpdateSubmission;
using OnlineJobMarketplace.Application.UseCases.Submissions.Queries.GetSubmission;
using PagedList;
using OnlineJobMarketplace.Application.UseCases.Submissions.Models;
using OnlineJobMarketplace.Application.UseCases.Submissions.Commands.CreateSubmission;
using OnlineJobMarketplace.Application.UseCases.Submissions.Queries.PaginatedSubmission;
using OnlineSubmissionMarketplace.Application.UseCases.Submissions.Commands.DeleteSubmission;

namespace OnlineJobMarketplace.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ApiBaseController
    {

        public SubmissionController(IAppCache appCache, IConfiguration configuration)
        {
            _appCache = appCache;
            _configuration = configuration;
        }

        [HttpPost]
        public async ValueTask<ActionResult<SubmissionDto>> CreateSubmissionAsync(CreateSubmissionCommand command)
            => Ok(await Mediator.Send(command));


        [HttpGet("{submissionId}")]
        public async ValueTask<ActionResult<SubmissionDto>> GetSubmissionAsync(Guid submissionId)
                 => await Mediator.Send(new GetSubmissionQuery(submissionId));

        [HttpGet]
        public async ValueTask<ActionResult<List<SubmissionDto>>> GetSubmissionsWithPaginated(int page = 1)
        {
            IPagedList<SubmissionDto> query = (await Mediator
                   .Send(new GetAllSubmissionQuery()))
                   .ToPagedList(page, 10);
            return Ok(query);
        }

        [HttpPut]
        public async ValueTask<ActionResult<SubmissionDto>> PutSubmissionAsync(UpdateSubmissionCommand command)
                => await Mediator.Send(command);

        [HttpDelete("{submissionId}")]
        public async ValueTask<ActionResult<SubmissionDto>> DeleteSubmissionAsync(Guid submissionId)
                => await Mediator.Send(new DeleteSubmissionCommand(submissionId));

    }
}
