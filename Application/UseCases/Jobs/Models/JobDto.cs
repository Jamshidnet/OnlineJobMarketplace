
namespace OnlineJobMarketplace.Application.UseCases.Jobs.Models
{
    public  class JobDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirments { get; set; }

    }
}
