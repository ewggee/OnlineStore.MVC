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
                .HasPrecision(8, 2)
                .IsRequired(true);

            builder.Property(p => p.CategoryId)
                .HasColumnName("category_id")
                .IsRequired(true);

            builder.Property(p => p.ImageUrl)
                .HasColumnName("image_url");

            builder.Property(p => p.StockQuantity)
                .HasColumnName("stock_quantity")
                .IsRequired(true);

            builder.Property(p => p.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired(true);

            builder.Property(p => p.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(p => p.IsDeleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            // Связи
            builder.HasMany(p => p.Images)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Сидирование
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "ROCKDALE Stars Precision Bass",
                    Description = "ROCKDALE Stars PB Bass – универсальная бас-гитара с формой корпуса пресижн бас (precision bass). Звукосниматель типа сплит-сингл (split-single) позволяет гитаре звучать особенно напористо, дает характерный мощный звук с особенно выраженными средними частотами. Корпус гитары изготовлен из тополя, гриф из клена с профилем C-Shape. Накладка грифа из HPL-композита - современного материала, устойчивого к резким изменениям температуры и влажности. В грифе установлен анкер для регулировки высоты струн. Струны 45-105 из никелированной стали. В комплект входит набор ключей для отстройки гитары, кабель для подключения Jack-Jack и инструкция по уходу за иструментом. Мензура 864мм.",
                    Price = 13599,
                    CategoryId = 5,
                    ImageUrl = "https://avatars.mds.yandex.net/get-mpic/1937077/img_id1984092563303990645.jpeg/optimize",
                    StockQuantity = 134,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 2,
                    Name = "ROCKDALE Stars HT HSS Black Limited Edition",
                    Description = "ROCKDALE Stars HT HSS – универсальная электрогитара, полностью выполненная в стильном черном цвете. Подходит для обучения. Форма корпуса стратокастер (stratocaster), керамические звукосниматели HSS, 5-ти позиционный переключатель, 2 ручки тона(tone), ручка громкости(volume) и фиксированный бридж (hardtail bridge) дают возможность исполнять любой стиль музыки. Корпус из тополя, гриф из клена с профилем C-Shape. Накладка из HPL- композита - современного материала, устойчивого к резким изменениям температуры и влажности. В грифе установлен анкер для регулировки высоты струн на грифом. Струны 10-46 из никелированной стали. В комплект входят ключи для отстройки гитары, кабель, для подключения Jack-Jack и инструкция по уходу за инструментом.",
                    Price = 11899,
                    CategoryId = 6,
                    ImageUrl = "https://avatars.mds.yandex.net/get-mpic/1522540/img_id8828176765638498087.jpeg/optimize",
                    StockQuantity = 98,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
