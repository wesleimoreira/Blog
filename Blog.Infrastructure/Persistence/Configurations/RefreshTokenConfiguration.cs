using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(refreshToken => refreshToken.Id);

            builder
                .Property(refreshToken => refreshToken.Id)
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            builder
                .Property(refreshToken => refreshToken.UserId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder
                .Property(refreshToken => refreshToken.TokenHash)
                .HasColumnType("nvarchar(500)")
                .HasMaxLength(500)
                .IsRequired();

            builder
                .Property(refreshToken => refreshToken.ExpiresAt)
                .HasColumnType("datetime2")
                .IsRequired();

            builder
                .Property(refreshToken => refreshToken.RevokedAt)
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder
                .Property(refreshToken => refreshToken.CreatedAt)
                .HasColumnType("datetime2")
                .ValueGeneratedNever()
                .IsRequired();

            builder
                .Property(refreshToken => refreshToken.UpdatedAt)
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder
                .HasIndex(refreshToken => refreshToken.TokenHash)
                .IsUnique();

            builder.HasIndex(refreshToken => refreshToken.UserId);

            builder
                .HasOne(refreshToken => refreshToken.User)
                .WithMany()
                .HasForeignKey(refreshToken => refreshToken.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
