using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");

        builder.HasKey(p => p.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.OrderId).IsRequired();
        builder.Property(s => s.ProductId).IsRequired();
        
        builder.Property(s => s.Amount)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(11,2)");
        
        builder.Property(s => s.Discount).HasColumnType("decimal(11,2)");

        builder.Property(s => s.TotalPrice)
            .IsRequired()
            .HasColumnType("decimal(11,2)");
        
        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Order)
            .WithMany(s => s.Items)
            .HasForeignKey(i => i.OrderId);
    }
}