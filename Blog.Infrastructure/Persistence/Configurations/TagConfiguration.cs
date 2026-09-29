using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");

            // Primary Key
            builder.HasKey(tag => tag.Id);

            builder
                .Property(tag => tag.Id)
                .HasColumnName("Id")
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            // Name
            builder
                .Property(tag => tag.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // Slug
            builder
                .Property(tag => tag.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("nvarchar(150)")
                .HasMaxLength(150);

            // CreatedAt
            builder
                .Property(tag => tag.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasColumnType("datetime2")
                .ValueGeneratedNever();

            // UpdatedAt
            builder
                .Property(tag => tag.UpdatedAt)
                .IsRequired(false)
                .HasColumnName("UpdatedAt")
                .HasColumnType("datetime2")
                .ValueGeneratedNever();

            // Unique Name
            builder
                .HasIndex(tag => tag.Name)
                .IsUnique();

            // Unique Slug
            builder
                .HasIndex(tag => tag.Slug)
                .IsUnique();
        }
    }
}
