using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            
            builder.Property(p=>p.Id).IsRequired();
            builder.Property(p=> p.Name).IsRequired().HasMaxLength(ProductConstants.MaxLengthProduct);
            builder.Property(p=> p.Description).IsRequired().HasMaxLength(ProductConstants.MaxLengthProductDescription);            
            builder.Property(p=> p.Price).HasColumnType(ProductConstants.HasColumnTypePriceInfo);
            builder.Property(p=> p.PictureUrl).IsRequired();
            builder.HasOne(b=> b.ProductBrand)
                .WithMany()
                .HasForeignKey(p=> p.ProductBrandId);
            builder.HasOne(t=> t.ProductType)
                .WithMany()
                .HasForeignKey(p=> p.ProductTypeId);
        } 
    }
}