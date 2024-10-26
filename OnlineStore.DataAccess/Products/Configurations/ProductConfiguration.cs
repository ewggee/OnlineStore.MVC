using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Products.Configurations
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(p => p.Id);
            builder.Property(pat => pat.Id)
                .HasColumnName("product_id");

            // Свойства
            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(1000)
                .IsRequired(true);

            builder.Property(p => p.Description)
                .HasColumnName("description")
                .IsRequired(true);

            builder.Property(p => p.Price)
                .HasColumnName("price")
                .HasPrecision(14, 4)
                .IsRequired(true);

            builder.Property(p => p.ImageUrl)
                .HasColumnName("image_url");

            builder.Property(p => p.StockQuantity)
                .HasColumnName("stock_quantity")
                .IsRequired(true);

            builder.Property(p => p.Created)
                .HasColumnName("created")
                .IsRequired(true);

            builder.Property(p => p.Updated)
                .HasColumnName("updated");

            builder.Property(p => p.IsDeleted)
                .HasColumnName("deleted");

            builder.Property(p => p.CategoryId)
                .HasColumnName("category_id");

            // Связи
            builder.HasMany(p => p.Images)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
