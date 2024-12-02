using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Carts.Configurations
{
    public sealed class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("carts");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("cart_id");

            // Свойства
            builder.Property(c => c.UserId)
                .HasColumnName("user_id");

            builder.Property(c => c.StatusId)
                .HasColumnName("status_id");

            builder.Property(c => c.Created)
                .HasColumnName("created")
                .IsRequired(true);

            builder.Property(c => c.Updated)
                .HasColumnName("updated");

            builder.Property(c => c.Closed)
                .HasColumnName("closed");

            // Связи
            builder.HasOne(c => c.Status)
                .WithMany()
                .HasForeignKey(c => c.StatusId)
                .IsRequired(true);
        }
    }
}
