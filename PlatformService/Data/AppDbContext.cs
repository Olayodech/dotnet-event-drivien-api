using Microsoft.EntityFrameworkCore;
using Models;

namespace PlatformService.Data {
    public class AppDbContext(DbContextOptions<AppDbContext> opt) : DbContext(opt) {
        public DbSet<PlatformModel> PlatformModels { get; set; }
    }
}