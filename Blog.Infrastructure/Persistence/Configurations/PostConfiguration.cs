using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");

            // Primary Key
            builder.HasKey(post => post.Id);

            builder
                .Property(post => post.Id)
                .HasColumnName("Id")
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            // Title
            builder
                .Property(post => post.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasColumnType("nvarchar(200)")
                .HasMaxLength(200);

            // Slug
            builder
                .Property(post => post.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("nvarchar(250)")
                .HasMaxLength(250);

            // Summary
            builder
                .Property(post => post.Summary)
                .IsRequired()
                .HasColumnName("Summary")
                .HasColumnType("nvarchar(500)")
                .HasMaxLength(500);

            // Content
            builder
                .Property(post => post.Content)
                .IsRequired()
                .HasColumnName("Content")
                .HasColumnType("nvarchar(max)");

            // IsPublished
            builder
                 .Property(post => post.IsPublished)
                 .IsRequired()
                 .HasColumnName("IsPublished")
                 .HasColumnType("bit")
                 .HasDefaultValue(false);

            // PublishedAt
            builder
                .Property(post => post.PublishedAt)
                .IsRequired(false)
                .HasColumnName("PublishedAt")
                .HasColumnType("datetime2");

            // AuthorId
            builder
                .Property(post => post.AuthorId)
                .IsRequired()
                .HasColumnName("AuthorId")
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            // CategoryId
            builder
                .Property(post => post.CategoryId)
                .IsRequired()
                .HasColumnName("CategoryId")
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            // CreatedAt
            builder
                .Property(post => post.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasColumnType("datetime2")
                .ValueGeneratedNever();

            // UpdatedAt
            builder
                .Property(post => post.UpdatedAt)
                .IsRequired(false)
                .HasColumnName("UpdatedAt")
                .HasColumnType("datetime2")
                .ValueGeneratedNever();

            // Unique Slug
            builder
                .HasIndex(post => post.Slug)
                .IsUnique();

            // Author (User) 1:N Posts
            builder
                .HasOne(post => post.Author)
                .WithMany(user => user.Posts)
                .HasForeignKey(post => post.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Category 1:N Posts
            builder
                .HasOne(post => post.Category)
                .WithMany(category => category.Posts)
                .HasForeignKey(post => post.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
