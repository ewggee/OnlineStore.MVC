using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Orders.Configurations
{
    public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasColumnName("order_id");

            // Свойства
            builder.Property(o => o.UserId)
                .HasColumnName("user_id")
                .IsRequired(true);

            builder.Property(o => o.OrderDate)
                .HasColumnName("order_date")
                .IsRequired(true);

            builder.Property(o => o.TotalPrice)
                .HasColumnName("total_price")
                .HasPrecision(18, 2)
                .IsRequired(true);

            builder.Property(o => o.StatusId)
                .HasColumnName("order_status_id")
                .IsRequired(true);

            // Связи
            builder.HasOne(o => o.Status)
                .WithMany()
                .HasForeignKey(o => o.StatusId)
                .IsRequired(true);

            builder.HasMany(o => o.Items)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
