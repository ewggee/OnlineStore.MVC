using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Contracts.ApplicationRoles;

namespace OnlineStore.DataAccess.Users.Configurations
{
    public sealed class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("application_roles");

            builder.HasKey(ar => ar.Id);

            // Сидирование
            builder.HasData(
                new ApplicationRole
                {
                    Id = 1,
                    Name = AppRoles.USER,
                    NormalizedName = AppRoles.USER.ToUpper(),
                },
                new ApplicationRole
                {
                    Id = 2,
                    Name = AppRoles.ADMIN,
                    NormalizedName = AppRoles.ADMIN.ToUpper(),
                }
            );
        }
    }
}
