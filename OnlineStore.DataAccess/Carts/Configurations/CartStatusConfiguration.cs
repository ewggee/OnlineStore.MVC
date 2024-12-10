using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Contracts.Enums;
using OnlineStore.Domain.Entities;
using OnlineStore.Core.Common.Extensions;

namespace OnlineStore.DataAccess.Carts.Configurations
{
    public sealed class CartStatusConfiguration : IEntityTypeConfiguration<CartStatus>
    {
        public void Configure(EntityTypeBuilder<CartStatus> builder)
        {
            builder.ToTable("cart_statuses");

            builder.HasKey(cs => cs.Id);
            builder.Property(cs => cs.Id)
                .HasColumnName("cart_status_id");

            // Свойства
            builder.Property(cs => cs.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(256);

            // Сидирование
            builder.HasData(
                Enum.GetValues(typeof(CartStatusEnum))
                .Cast<CartStatusEnum>()
                .Select(e => new CartStatus
                {
                    Id = (int)e,
                    Name = e.GetEnumDescription()
                })
            );
        }
    }
}
