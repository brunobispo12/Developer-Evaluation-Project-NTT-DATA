using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<User>(builder =>
        {
            builder.OwnsOne(u => u.Name, n =>
            {
                n.Property(nn => nn.Firstname)
                    .HasMaxLength(50)
                    .IsRequired();

                n.Property(nn => nn.Lastname)
                    .HasMaxLength(50)
                    .IsRequired();
            });

            builder.OwnsOne(u => u.Address, a =>
            {
                a.Property(ad => ad.City)
                    .IsRequired();

                a.Property(ad => ad.Street)
                    .IsRequired();

                a.Property(ad => ad.Number)
                    .IsRequired();

                a.Property(ad => ad.Zipcode)
                    .IsRequired();

                a.OwnsOne(ad => ad.Geolocation, g =>
                {
                    g.Property(x => x.Lat)
                        .IsRequired();

                    g.Property(x => x.Long)
                        .IsRequired();
                });
            });
        });

        modelBuilder.Entity<Sale>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<SaleItem>()
            .HasKey(si => si.Id);

        modelBuilder.Entity<Sale>()
            .HasMany(s => s.Items)
            .WithOne(si => si.Sale)
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }

}
public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(
               connectionString,
               b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
        );

        return new DefaultContext(builder.Options);
    }
}