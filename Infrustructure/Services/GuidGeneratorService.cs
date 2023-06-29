using OnlineJobMarketplace.Application.Common.Interfaces;

namespace OnlineJobMarketplace.Infrustructure.Services;

public class GuidGeneratorService : IGuidGenerator
{
    public Guid Guid => Guid.NewGuid();
}
