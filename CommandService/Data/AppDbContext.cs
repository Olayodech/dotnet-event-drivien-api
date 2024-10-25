using Microsoft.EntityFrameworkCore;
using CommandService.Models;
namespace CommandService.Data
{
    
    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<PlatformModels> PlatformModel { get; set; }

        public DbSet<CommandModel> CommandModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlatformModels>()
                .HasMany(p => p.Commands)
                .WithOne(c => c.Platform!)
                .HasForeignKey(c => c.PlatformId);

            modelBuilder.Entity<CommandModel>()
                .HasOne(c => c.Platform)
                .WithMany(p => p.Commands)
                .HasForeignKey(c => c.PlatformId);

        }
    }
}