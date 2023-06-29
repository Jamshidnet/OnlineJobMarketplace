using OnlineJobMarketplace.Domein.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineJobMarketplace.Application.UseCases.Companies.Models
{
    public record CompanyWithJobsDto : CompanyDto
    {
        ICollection<Job> Jobs { get; set; }
    }
}
