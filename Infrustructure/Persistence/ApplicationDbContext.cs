using Microsoft.EntityFrameworkCore;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Domein.Entities;
using OnlineJobMarketplace.Infrustructure.Persistence.Interceptors;
using System.Reflection;

namespace OnlineJobMarketplace.Infrustructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

       public DbSet<Candidate> Candidates { get; set; }
       public DbSet<Company> Companies { get;set; }
       public DbSet<Job> Jobs { get;set; }
       public DbSet<Skill> Skills { get;set; }
       public DbSet<Submission> Submissions { get;set; }


        private readonly AuditableEntitySaveChangesInterceptor _interceptor;


        private readonly DbContextOptions<ApplicationDbContext>? options;
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>? options,
            AuditableEntitySaveChangesInterceptor interceptor) : base(options)
        {
            _interceptor=interceptor;
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entity.Name).Property(typeof(DateTimeOffset), "CreatedDate")
                    .HasColumnType("timestamptz");

                modelBuilder.Entity(entity.Name).Property(typeof(DateTimeOffset), "UpdatedDate")
                    .HasColumnType("timestamptz");
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_interceptor);
        }
    }

}

