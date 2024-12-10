using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Contracts.Enums;
using OnlineStore.Core.Common.Extensions;

namespace OnlineStore.DataAccess.Notifications.Configurations
{
    public sealed class NotificationChannelConfiguration : IEntityTypeConfiguration<NotificationChannel>
    {
        public void Configure(EntityTypeBuilder<NotificationChannel> builder)
        {
            builder.ToTable("notification_channels");

            builder.HasKey(nc => nc.Id);
            builder.Property(nc => nc.Id)
                .HasColumnName("notification_channel_id");

            builder.Property(nc => nc.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(256);

            // Сидирование
            builder.HasData(
                Enum.GetValues(typeof(NotificationChannelEnum))
                .Cast<NotificationChannelEnum>()
                .Select(e => new NotificationChannel
                {
                    Id = (int)e,
                    Name = e.GetEnumDescription()
                })
            );
        }
    }
}
