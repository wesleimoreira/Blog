using Blog.Domain.Common;
using Blog.Domain.Entities;

namespace Blog.Domain.Tests.Entities
{
    public class PostTests
    {
        [Fact]
        public void Constructor_ShouldCreatePost_WhenDataIsValid()
        {
            // Arrange
            var title = "Building APIs with .NET";
            var summary = "An introduction to API development";
            var content = "Post content...";
            var authorId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();

            // Act
            var post = new Post(
                title,
                summary,
                content,
                authorId,
                categoryId);

            // Assert
            Assert.Equal(title, post.Title);
            Assert.Equal("building-apis-with-net", post.Slug);
            Assert.Equal(summary, post.Summary);
            Assert.Equal(content, post.Content);
            Assert.Equal(authorId, post.AuthorId);
            Assert.Equal(categoryId, post.CategoryId);
            Assert.False(post.IsPublished);
            Assert.Null(post.PublishedAt);
            Assert.NotEqual(Guid.Empty, post.Id);
        }

        [Fact]
        public void Publish_ShouldSetPublishedStatusAndDate()
        {
            // Arrange
            var post = new Post(
                "Building APIs with .NET",
                "An introduction to API development",
                "Post content...",
                Guid.NewGuid(),
                Guid.NewGuid());

            // Act
            post.Publish();

            // Assert
            Assert.True(post.IsPublished);
            Assert.NotNull(post.PublishedAt);
            Assert.True(post.PublishedAt <= DateTime.UtcNow);
            Assert.NotNull(post.UpdatedAt);
        }

        [Fact]
        public void Unpublish_ShouldClearPublishedStatusAndDate()
        {
            // Arrange
            var post = new Post(
                "Building APIs with .NET",
                "An introduction to API development",
                "Post content...",
                Guid.NewGuid(),
                Guid.NewGuid());

            post.Publish();

            // Act
            post.Unpublish();

            // Assert
            Assert.False(post.IsPublished);
            Assert.Null(post.PublishedAt);
            Assert.NotNull(post.UpdatedAt);
        }

        [Fact]
        public void SetTags_ShouldAssociateTags_WhenTagIdsAreValid()
        {
            // Arrange
            var post = new Post(
                "Building APIs with .NET",
                "An introduction to API development",
                "Post content...",
                Guid.NewGuid(),
                Guid.NewGuid());

            var tagIds = new[]
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            // Act
            post.SetTags(tagIds);

            // Assert
            Assert.Equal(2, post.PostTags.Count);
            Assert.Contains(post.PostTags, x => tagIds.Contains(x.TagId));
        }

        [Fact]
        public void SetTags_ShouldNotDuplicateTags_WhenIdsAreRepeated()
        {
            // Arrange
            var post = new Post(
                "Building APIs with .NET",
                "An introduction to API development",
                "Post content...",
                Guid.NewGuid(),
                Guid.NewGuid());

            var tagId = Guid.NewGuid();

            // Act
            post.SetTags([tagId, tagId]);

            // Assert
            Assert.Single(post.PostTags);
            Assert.Equal(tagId, post.PostTags.First().TagId);
        }

        [Fact]
        public void SetTags_ShouldReplacePreviousTags_WhenCalledAgain()
        {
            // Arrange
            var post = new Post(
                "Building APIs with .NET",
                "An introduction to API development",
                "Post content...",
                Guid.NewGuid(),
                Guid.NewGuid());

            var oldTagId = Guid.NewGuid();
            var newTagId = Guid.NewGuid();

            post.SetTags([oldTagId]);

            // Act
            post.SetTags([newTagId]);

            // Assert
            Assert.Single(post.PostTags);
            Assert.Equal(newTagId, post.PostTags.First().TagId);
            Assert.DoesNotContain(post.PostTags, x => x.TagId == oldTagId);
        }

        [Fact]
        public void Constructor_ShouldThrowDomainException_WhenTitleIsEmpty()
        {
            // Arrange
            var summary = "An introduction to API development";
            var content = "Post content...";

            // Act
            var exception = Assert.Throws<DomainException>(
                () => new Post(
                    string.Empty,
                    summary,
                    content,
                    Guid.NewGuid(),
                    Guid.NewGuid()));

            // Assert
            Assert.NotNull(exception);
        }
    }
}
