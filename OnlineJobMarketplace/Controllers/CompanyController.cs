using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineJobMarketplace.Application.UseCases.Companies.Commands.DeleteCompany;
using OnlineJobMarketplace.Application.UseCases.Companies.Commands.UpdateCompany;
using OnlineJobMarketplace.Application.UseCases.Companies.Models;
using OnlineJobMarketplace.Application.UseCases.Companies.Queries.GetAllCompanies;
using OnlineJobMarketplace.Application.UseCases.Companies.Queries.GetCompany;
using OnlineJobMarketplace.Application.UseCases.Skills.Commands.CreateSkill.CreateSkill;
using PagedList;

namespace OnlineJobMarketplace.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ApiBaseController
{

    public CompanyController(IAppCache appCache, IConfiguration configuration)
    {
        _appCache = appCache;
        _configuration = configuration;
    }

    [HttpPost]
    public async ValueTask<ActionResult<CompanyDto>> CreateCompanyAsync(CreateCompanyCommand command)
        => Ok(await Mediator.Send(command));


    [HttpGet("{companyId}")]
    public async ValueTask<ActionResult<CompanyWithJobsDto>> GetCompanyAsync(Guid companyId)
             => await Mediator.Send(new GetCompanyQuery(companyId));

    [HttpGet]
    public async ValueTask<ActionResult<List<CompanyDto>>> GetCompanysWithPaginated(int page = 1)
    {
        IPagedList<CompanyDto> query = (await Mediator
               .Send(new GetAllCompanyQuery()))
               .ToPagedList(page, 10);
        return Ok(query);
    }

    [HttpPut]
    public async ValueTask<ActionResult<CompanyDto>> PutCompanyAsync(UpdateCompanyCommand command)
            => await Mediator.Send(command);

    [HttpDelete("{companyId}")]
    public async ValueTask<ActionResult<CompanyDto>> DeleteCompanyAsync(Guid companyId)
            => await Mediator.Send(new DeleteCompanyCommand(companyId));

}
