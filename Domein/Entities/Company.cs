using OnlineJobMarketplace.Domein.Common.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineJobMarketplace.Domein.Entities
{
    public class Company : BaseAuditableEntity
    {
        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Location { get; set; }

        public string  WebSite { get; set; }

        public ICollection<Job> Jobs { get; set; }

    }
}
