using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            // Primary Key
            builder.HasKey(user => user.Id);

            builder
                .Property(user => user.Id)
                .HasColumnName("Id")
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            // FirstName
            builder
                .Property(user => user.FirstName)
                .IsRequired()
                .HasColumnName("FirstName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // LastName
            builder
                .Property(user => user.LastName)
                .IsRequired()
                .HasColumnName("LastName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // Email
            builder
                .Property(user => user.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);

            // PasswordHash
            builder
                .Property(user => user.PasswordHash)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("nvarchar(500)")
                .HasMaxLength(500);

            // RoleId
            builder
                .Property(user => user.RoleId)
                .IsRequired()
                .HasColumnName("RoleId")
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            // CreatedAt
            builder
                .Property(user => user.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasColumnType("datetime2")
                .ValueGeneratedNever();

            // UpdatedAt
            builder
                .Property(user => user.UpdatedAt)
                .IsRequired(false)
                .HasColumnName("UpdatedAt")
                .HasColumnType("datetime2")
                .ValueGeneratedNever();

            // Unique Email
            builder
                .HasIndex(user => user.Email)
                .IsUnique();

            // Role 1:N User
            builder
                .HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
