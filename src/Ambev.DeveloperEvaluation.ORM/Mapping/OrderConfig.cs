using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(p => p.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.OrderNumber)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(s => s.TotalAmount).IsRequired();
        builder.Property(s => s.UserId).IsRequired();
        builder.Property(s => s.Branch).IsRequired().HasMaxLength(100);

        builder.Property(s => s.IsCancelled)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(s => s.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(s => s.OrderNumber).IsUnique();
    }
}