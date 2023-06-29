using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineJobMarketplace.Domein.Enums
{
    public enum SubmissionStatus : byte
    {
        NotViewed =1,
        Watched =2,
        Rejected =3,
        Accepted =4
    }
}
