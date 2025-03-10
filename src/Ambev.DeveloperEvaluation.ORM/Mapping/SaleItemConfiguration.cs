using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.HasKey(si => si.Id);

        builder.Property(si => si.Product)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(si => si.Quantity)
            .IsRequired();

        builder.Property(si => si.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(si => si.Discount)
            .HasColumnType("decimal(5,2)")
            .IsRequired();

        builder.Ignore(si => si.TotalPrice);

        builder.HasOne<Sale>()
            .WithMany(s => s.Items)
            .HasForeignKey("SaleId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
