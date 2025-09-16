using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nontitle_BusinessObject.Models;

namespace Nontitle_BusinessObject.Context
{
    public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Category> Categories {  get; set; }
        public DbSet<OrderCheck> OrderChecks {  get; set; }
        public DbSet<OrderCheckItem> OrderCheckItems {  get; set; }
        public DbSet<Product> Products {  get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(options =>
            {
                
            });

            builder.Entity<Category>(options =>
            {
                options.HasQueryFilter(x => !x.IsDeleted);
            });

            builder.Entity<OrderCheck>(options =>
            {
                options.HasQueryFilter(x => !x.IsDeleted);
            });

            builder.Entity<OrderCheckItem>(options =>
            {
                options.HasQueryFilter(x => !x.IsDeleted);
            });

            builder.Entity<Product>(options =>
            {
                options.HasQueryFilter(x => !x.IsDeleted);
            });

            builder.HasDefaultSchema("nontitle");
        }
    }
}
