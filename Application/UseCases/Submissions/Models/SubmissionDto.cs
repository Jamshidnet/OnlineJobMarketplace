using OnlineJobMarketplace.Domein.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineJobMarketplace.Application.UseCases.Submissions.Models
{
    public  record SubmissionDto
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }

        public Guid CandidateId { get; set; }

        public SubmissionStatus Status { get; set; }


    }
}
