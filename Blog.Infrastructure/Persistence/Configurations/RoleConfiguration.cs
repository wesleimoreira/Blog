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
        }
    }
}
