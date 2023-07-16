using Microsoft.EntityFrameworkCore;
using OnlineJobMarketplace.Domein.Entities;

namespace OnlineJobMarketplace.Application.Common.Interfaces;

public  interface IApplicationDbContext
{
    DbSet<T> Set<T>() where T : class;
    DbSet<Candidate> Candidates { get; }
    DbSet<Company> Companies { get; }
    DbSet<Job> Jobs { get; }
    DbSet<Skill> Skills { get; }
    DbSet<Submission> Submissions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
