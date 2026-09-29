using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            // Primary Key
            builder.HasKey(category => category.Id);

            builder
                .Property(category => category.Id)
                .HasColumnName("Id")
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            // Name
            builder
                .Property(category => category.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // Slug
            builder
                .Property(category => category.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("nvarchar(150)")
                .HasMaxLength(150);

            // Description
            builder
                .Property(category => category.Description)
                .IsRequired(false)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(500)")
                .HasMaxLength(500);

            // CreatedAt
            builder
                .Property(category => category.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasColumnType("datetime2")
                .ValueGeneratedNever();

            // UpdatedAt
            builder
                .Property(category => category.UpdatedAt)
                .IsRequired(false)
                .HasColumnName("UpdatedAt")
                .HasColumnType("datetime2")
                .ValueGeneratedNever();

            // Unique Name
            builder
                .HasIndex(category => category.Name)
                .IsUnique();

            // Unique Slug
            builder
                .HasIndex(category => category.Slug)
                .IsUnique();
        }
    }
}
