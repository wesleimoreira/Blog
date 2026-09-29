using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            // Primary Key
            builder.HasKey(comment => comment.Id);

            builder
                .Property(comment => comment.Id)
                .HasColumnName("Id")
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            // Content
            builder
                .Property(comment => comment.Content)
                .IsRequired()
                .HasColumnName("Content")
                .HasColumnType("nvarchar(1000)")
                .HasMaxLength(1000);

            // IsApproved
            builder
                .Property(comment => comment.IsApproved)
                .IsRequired()
                .HasColumnName("IsApproved")
                .HasColumnType("bit")
                .HasDefaultValue(false);

            // PostId
            builder
                .Property(comment => comment.PostId)
                .IsRequired()
                .HasColumnName("PostId")
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            // UserId
            builder
                .Property(comment => comment.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            // CreatedAt
            builder
                .Property(comment => comment.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasColumnType("datetime2")
                .ValueGeneratedNever();

            // UpdatedAt
            builder
                .Property(comment => comment.UpdatedAt)
                .IsRequired(false)
                .HasColumnName("UpdatedAt")
                .HasColumnType("datetime2")
                .ValueGeneratedNever();

            // Post 1:N Comments
            builder
                .HasOne(comment => comment.Post)
                .WithMany(post => post.Comments)
                .HasForeignKey(comment => comment.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // User 1:N Comments
            builder
                .HasOne(comment => comment.User)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
