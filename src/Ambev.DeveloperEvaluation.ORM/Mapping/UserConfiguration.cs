using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Phone)
                .HasMaxLength(20);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(u => u.Role)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.OwnsOne(u => u.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("FirstName");

                nameBuilder.Property(n => n.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LastName");
            });


            builder.OwnsOne(u => u.Address, addressBuilder =>
            {
                addressBuilder.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("City");

                addressBuilder.Property(a => a.Street)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Street");

                addressBuilder.Property(a => a.Number)
                    .IsRequired()
                    .HasColumnName("Number");

                addressBuilder.Property(a => a.Zipcode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Zipcode");

                addressBuilder.OwnsOne(a => a.Geolocation, geoBuilder =>
                {
                    geoBuilder.Property(g => g.Lat)
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnName("Latitude");

                    geoBuilder.Property(g => g.Long)
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnName("Longitude");
                });
            });
        }
    }
}