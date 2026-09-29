using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Configurations
{
    public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            builder.ToTable("PostTags");

            // Composite Primary Key
            builder.HasKey(postTag => new
            {
                postTag.PostId,
                postTag.TagId
            });

            // PostId
            builder
                .Property(postTag => postTag.PostId)
                .IsRequired()
                .HasColumnName("PostId")
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            // TagId
            builder
                .Property(postTag => postTag.TagId)
                .IsRequired()
                .HasColumnName("TagId")
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            // Post 1:N PostTags
            builder
                .HasOne(postTag => postTag.Post)
                .WithMany(post => post.PostTags)
                .HasForeignKey(postTag => postTag.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Tag 1:N PostTags
            builder
                .HasOne(postTag => postTag.Tag)
                .WithMany(tag => tag.PostTags)
                .HasForeignKey(postTag => postTag.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
