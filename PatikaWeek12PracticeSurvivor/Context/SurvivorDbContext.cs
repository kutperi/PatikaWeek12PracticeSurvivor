using Microsoft.EntityFrameworkCore;
using PatikaWeek12PracticeSurvivor.Entities;

namespace PatikaWeek12PracticeSurvivor.Context
{
    public class SurvivorDbContext : DbContext
    {
        public SurvivorDbContext(DbContextOptions<SurvivorDbContext> options) : base(options) { }
        

        public DbSet<CompetitorEntity> Competitors => Set<CompetitorEntity>();
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompetitorEntity>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Competitors)
                .HasForeignKey(c => c.CategoryId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
