using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.ProductAttributes.Configurations
{
    public sealed class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.ToTable("attributes");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .HasColumnName("attribute_id");

            // Свойства
            builder.Property(a => a.Name)
                .HasColumnName("name")
                .IsRequired(true);

            // Сидирование
            builder.HasData(
                new ProductAttribute
                {
                    Id = 1,
                    Name = "Цвет"
                },
                new ProductAttribute
                {
                    Id = 2,
                    Name = "Размер"
                }
            );
        }
    }
}
