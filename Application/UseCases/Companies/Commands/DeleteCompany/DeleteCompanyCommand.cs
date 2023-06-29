using AutoMapper;
using MediatR;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Companies.Models;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineJobMarketplace.Application.UseCases.Companies.Commands.DeleteCompany
{
    public record DeleteCompanyCommand(Guid Id) : IRequest<CompanyDto>;

    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, CompanyDto>
    {
        private IApplicationDbContext _dbContext;
        private IMapper _mapper;

        public DeleteCompanyCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CompanyDto> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            Company company = FilterIfCompanyExsists(request.Id);

            _dbContext.Companies.Remove(company);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CompanyDto>(company);
        }

        private Company FilterIfCompanyExsists(Guid id)
         =>  _dbContext.Companies.FirstOrDefault(c => c.Id == id)
            ?? throw new NotFoundException(
                " There is no company with this id. ");
    }



}
