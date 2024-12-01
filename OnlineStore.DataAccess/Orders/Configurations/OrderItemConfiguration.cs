using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Orders.Configurations
{
    public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_items");

            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.Id)
                .HasColumnName("order_item_id");

            // Свойства
            builder.Property(oi => oi.OrderId)
                .HasColumnName("order_id")
                .IsRequired(true);

            builder.Property(oi => oi.ProductId)
                .HasColumnName("product_id")
                .IsRequired(true);

            builder.Property(oi => oi.Quantity)
                .HasColumnName("quantity")
                .IsRequired(true);

            builder.Property(oi => oi.UnitPrice)
                .HasColumnName("unit_price")
                .IsRequired(true);

            // Связи
            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
