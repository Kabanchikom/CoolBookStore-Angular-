using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Account.Models;
using Web.Areas.Author.Models;
using Web.Areas.Cart.Models;
using Web.Areas.Manufacturer.Models;
using Web.Areas.Order.Models;
using Web.Areas.Product.Models;

namespace Web.EF;

public class BookStoreDbContext : IdentityDbContext<User>
{
    public DbSet<ProductCard> Products { get; set; }
    public DbSet<BookCard> Books { get; set; }
    public DbSet<StationeryCard> Stationary { get; set; }
    public DbSet<ManufacturerCard> Manufacturers { get; set; }
    public DbSet<OrderCard> Orders { get; set; }
    public DbSet<AuthorCard> Authors { get; set; }
    public DbSet<CartLine> CartLines { get; set; }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ProductCard>()
            .Property(x => x.Rating)
            .HasPrecision(3, 2);
    }
}