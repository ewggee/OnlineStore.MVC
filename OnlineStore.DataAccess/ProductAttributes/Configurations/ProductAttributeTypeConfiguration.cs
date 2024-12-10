using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Contracts.Enums;
using OnlineStore.Core.Common.Extensions;

namespace OnlineStore.DataAccess.ProductAttributes.Configurations
{
    public sealed class ProductAttributeTypeConfiguration : IEntityTypeConfiguration<ProductAttributeType>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeType> builder)
        {
            builder.ToTable("attribute_types");

            builder.HasKey(pat => pat.Id);
            builder.Property(pat => pat.Id)
                .HasColumnName("attribute_type_id");

            // Свойства
            builder.Property(pat => pat.Name)
                .HasColumnName("name")
                .IsRequired(true);

            // Сидирование
            builder.HasData(
                Enum.GetValues(typeof(ProductAttributeTypeEnum))
                .Cast<ProductAttributeTypeEnum>()
                .Select(e => new ProductAttributeType
                {
                    Id = (int)e,
                    Name = e.GetEnumDescription()
                })
            );
        }
    }
}
