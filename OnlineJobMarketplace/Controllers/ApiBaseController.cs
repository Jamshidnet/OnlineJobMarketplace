using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OnlineJobMarketplace.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        private IMediator? _mediator;
        public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        public IAppCache? _appCache;

        public IConfiguration? _configuration;

    }
}
