using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Contracts.Enums;
using OnlineStore.Core.Common.Extensions;

namespace OnlineStore.DataAccess.Orders.Configurations
{
    public sealed class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("order_statuses");

            builder.HasKey(os => os.Id);
            builder.Property(os => os.Id)
                .HasColumnName("order_status_id");

            // Свойства
            builder.Property(os => os.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired(true);

            // Сидирование
            builder.HasData(
                Enum.GetValues(typeof(OrdersStatusEnum))
                .Cast<OrdersStatusEnum>()
                .Select(e => new OrderStatus
                {
                    Id = (int)e,
                    Name = e.GetEnumDescription()
                })
            );
        }
    }
}
