using Blog.Domain.Common;
using Blog.Domain.Entities;

namespace Blog.Domain.Tests.Entities
{
    public class CommentTests
    {
        [Fact]
        public void Constructor_ShouldCreateComment_WhenDataIsValid()
        {
            // Arrange
            var content = "Great article!";
            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            // Act
            var comment = new Comment(content, postId, userId);

            // Assert
            Assert.Equal(content, comment.Content);
            Assert.Equal(postId, comment.PostId);
            Assert.Equal(userId, comment.UserId);
            Assert.False(comment.IsApproved);
            Assert.NotEqual(Guid.Empty, comment.Id);
        }

        [Fact]
        public void UpdateContent_ShouldUpdateComment_WhenContentIsValid()
        {
            // Arrange
            var comment = new Comment(
                "Old comment",
                Guid.NewGuid(),
                Guid.NewGuid());

            var newContent = "Updated comment";

            // Act
            comment.UpdateContent(newContent);

            // Assert
            Assert.Equal(newContent, comment.Content);
            Assert.NotNull(comment.UpdatedAt);
        }

        [Fact]
        public void Approve_ShouldSetIsApprovedToTrue()
        {
            // Arrange
            var comment = new Comment(
                "Great article!",
                Guid.NewGuid(),
                Guid.NewGuid());

            // Act
            comment.Approve();

            // Assert
            Assert.True(comment.IsApproved);
            Assert.NotNull(comment.UpdatedAt);
        }

        [Fact]
        public void Reject_ShouldSetIsApprovedToFalse()
        {
            // Arrange
            var comment = new Comment(
                "Great article!",
                Guid.NewGuid(),
                Guid.NewGuid());

            comment.Approve();

            // Act
            comment.Reject();

            // Assert
            Assert.False(comment.IsApproved);
            Assert.NotNull(comment.UpdatedAt);
        }

        [Fact]
        public void Constructor_ShouldThrowDomainException_WhenContentIsEmpty()
        {
            // Act
            var exception = Assert.Throws<DomainException>(
                () => new Comment(
                    string.Empty,
                    Guid.NewGuid(),
                    Guid.NewGuid()));

            // Assert
            Assert.NotNull(exception);
        }
    }
}
