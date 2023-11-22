using MarioAuth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarioAuth
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public DbSet<News> News { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
            Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<MarioAuth.Models.Catalog>? Catalog { get; set; }
        public DbSet<MarioAuth.Models.Product>? Product { get; set; }
        public DbSet<MarioAuth.Models.MasterClass>? MasterClass { get; set; }
        }
}
