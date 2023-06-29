using OnlineJobMarketplace.Domein.Common.BaseEntities;
using OnlineJobMarketplace.Domein.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineJobMarketplace.Domein.Entities
{
    public class Submission : BaseAuditableEntity
    {
        public Guid JobId { get; set; }

        public Guid CandidateId { get; set; }

        public SubmissionStatus Status { get; set; }


        [ForeignKey(nameof(JobId))]
        public Job Job { get; set; }

        [ForeignKey(nameof(CandidateId))]
        public Candidate Candidate { get; set; }
    }
}
