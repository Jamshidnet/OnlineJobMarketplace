using OnlineJobMarketplace.Domein.Common.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineJobMarketplace.Domein.Entities
{
    public class Skill : BaseAuditableEntity
    {
        public string SkillName { get; set; }

        public ICollection<Candidate> Candidates { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }
}
