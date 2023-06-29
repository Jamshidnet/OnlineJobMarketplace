using OnlineJobMarketplace.Application.Common.Interfaces;

namespace OnlineJobMarketplace.Infrustructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
