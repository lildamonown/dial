using Kursa4.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Car> Cars { get; set; }

    public DbSet<Order> Orders { get; set; }
    
    public DbSet<Report> Reports { get; set; }
    
    public DbSet<Review> Reviews { get; set; }
        
    public DbSet<Service> Services { get; set; }
    
    public DbSet<Statistic> Statistics { get; set; }
    
    public DbSet<Status> Statuses { get; set; }
    
    public DbSet<Subservice> Subservices { get; set; }

    public DbSet<CarBrand> CarBrands { get; set; }

    public DbSet<CarSeries> CarSeries { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Subservices)
            .WithMany(s => s.Orders)
            .UsingEntity<Dictionary<string, object>>(
                "SubservicesOrders",
                j => j.HasOne<Subservice>().WithMany().HasForeignKey("SubserviceId"),
                j => j.HasOne<Order>().WithMany().HasForeignKey("OrderId"),
                j => j.ToTable("SubservicesOrders"));
    }
}