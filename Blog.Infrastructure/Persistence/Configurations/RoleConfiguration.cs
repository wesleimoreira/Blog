using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(role => role.Id);

            builder
                .Property(role => role.Id)
                .HasColumnName("Id")
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            builder
                .Property(role => role.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder
                .HasIndex(role => role.Name)
                .IsUnique();

            var createdAt = new DateTime(2026, 10, 1, 0, 0, 0, DateTimeKind.Utc);

            builder.HasData(
            new
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Admin",
                CreatedAt = createdAt,
                UpdatedAt = (DateTime?)null
            },
            new
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Author",
                CreatedAt = createdAt,
                UpdatedAt = (DateTime?)null
            },
            new
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Reader",
                CreatedAt = createdAt,
                UpdatedAt = (DateTime?)null
            });
        }
    }
}
