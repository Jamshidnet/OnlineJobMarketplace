using OnlineJobMarketplace.Domein.Common.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineJobMarketplace.Domein.Entities
{
    public class Job : BaseAuditableEntity
    {
        public Guid CompanyId { get; set; }
        public string Title { get; set; }
        public string  Description { get; set; }
        public string Requirments { get; set; }

        public  ICollection<Skill> RequiredSkills { get; set; }
        
        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; }

    }
}
