using OnlineJobMarketplace.Domein.Common.BaseEntities;
using OnlineJobMarketplace.Domein.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OnlineJobMarketplace.Domein.Entities
{
    public class Candidate : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string  Email { get; set; }

        public string Password { get; set; }

        public DateTime Birthdate { get; set; }

        public string ImageName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal ExperienceDuration { get; set; }

        public Gender  Gender { get; set; }
        public string Education { get; set; }
        public ICollection<Skill> Skills { get; set; }
    }
}
