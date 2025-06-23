using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dsw2025Tpi.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Sku)
                .IsRequired()
                .HasMaxLength(16);
            builder.HasIndex(p => p.Sku).IsUnique();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(p => p.Description)
                .HasMaxLength(100);
            builder.Property(p => p.CurrentUnitPrice)
                .IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.StockQuantity)
                .IsRequired();

            builder.Property(p=> p.IsActive)
                .IsRequired();




        }
    }
}
