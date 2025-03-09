
using Microsoft.EntityFrameworkCore;
using Survivor.Entities;

namespace Survivor.Context
{
    public class SurvivorDbContext : DbContext
    {
        public SurvivorDbContext(DbContextOptions<SurvivorDbContext> options) : base(options)
        {
            
        }

        //DbSetler
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
        public DbSet<CompetitorsEntity> Competitors => Set<CompetitorsEntity>();
    }
}
