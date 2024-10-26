using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Carts.Configurations
{
    public sealed class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.ToTable("cart_product");

            builder.HasKey(cp => cp.Id);
            builder.Property(cp => cp.Id)
                .HasColumnName("cart_product_id");

            // Свойства
            builder.Property(cp => cp.ProductId)
                .HasColumnName("product_id");

            builder.Property(cp => cp.CartId)
                .HasColumnName("cart_id");

            builder.Property(cp => cp.Quantity)
                .HasColumnName("quantity");

            // Связи
            builder.HasOne(cp => cp.Cart)
                .WithMany(c => c.Products)
                .HasForeignKey(cp => cp.CartId)
                .IsRequired(true);

            builder.HasOne(cp => cp.Product)
                .WithMany()
                .HasForeignKey(cp => cp.ProductId)
                .IsRequired(true);
        }
    }
}
