using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nontitle_BusinessObject.Models;

namespace Nontitle_BusinessObject.Context;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<OrderCheck> OrderChecks { get; set; }
    public DbSet<OrderCheckItem> OrderCheckItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductIngredient> ProductIngredients { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<StoreRole> StoresRoles { get; set; }
    public DbSet<UserStoreRole> UserStoreRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(options =>
        {
            options.HasMany(x => x.Stores)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<Category>(options =>
        {
            options.ToTable("Category");

            options.HasMany(x => x.Products)
                   .WithOne(x => x.Category)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.NoAction);

            options.HasQueryFilter(x => !x.IsDeleted);
        });

        builder.Entity<Ingredient>(options =>
        {
            options.ToTable("Ingredient");

            options.HasQueryFilter(x => !x.IsDeleted);
        });

        builder.Entity<OrderCheck>(options =>
        {
            options.ToTable("OrderCheck");

            options.HasQueryFilter(x => !x.IsDeleted);

            options.HasMany(x => x.Items)
            .WithOne(x => x.OrderCheck)
            .HasForeignKey(x => x.OrderCheckId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<OrderCheckItem>(options =>
        {
            options.ToTable("OrderCheckItem");

            options.HasQueryFilter(x => !x.IsDeleted);
        });

        builder.Entity<Product>(options =>
        {
            options.ToTable("Product");

            options.HasQueryFilter(x => !x.IsDeleted);

            options.HasMany(x => x.ProductIngredients)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<ProductIngredient>(options =>
        {
            options.ToTable("ProductIngredient");

            options.HasKey(x => new { x.ProductId, x.IngredientId });
        });

        builder.Entity<Store>(options =>
        {
            options.ToTable("Store");

            options.HasMany(x => x.Roles)
                   .WithOne(x => x.Store)
                   .HasForeignKey(x => x.StoreId)
                   .OnDelete(DeleteBehavior.Restrict);

            options.HasQueryFilter(x => !x.IsDeleted);
        });

        builder.Entity<StoreRole>(options =>
        {
            options.ToTable("StoreRole");

            options.HasQueryFilter(x => !x.IsDeleted);
        });

        builder.Entity<UserStoreRole>(options =>
        {
            options.ToTable("UserStoreRole");
            options.HasKey(x => new { x.UserId, x.StoreId, x.StoreRoleId });
        });

        builder.HasDefaultSchema("nontitle");
    }
}

