using Blog.Domain.Common;
using Blog.Domain.Entities;

namespace Blog.Domain.Tests.Entities
{
    public class TagTests
    {
        [Fact]
        public void Constructor_ShouldCreateTag_WhenDataIsValid()
        {
            // Arrange
            var name = "Dot Net";

            // Act
            var tag = new Tag(name);

            // Assert
            Assert.Equal(name, tag.Name);
            Assert.Equal("dot-net", tag.Slug);
            Assert.NotEqual(Guid.Empty, tag.Id);
        }

        [Fact]
        public void UpdateName_ShouldUpdateTag_WhenNameIsValid()
        {
            // Arrange
            var tag = new Tag("Dot Net");

            // Act
            tag.UpdateName("C Sharp");

            // Assert
            Assert.Equal("C Sharp", tag.Name);
            Assert.Equal("c-sharp", tag.Slug);
            Assert.NotNull(tag.UpdatedAt);
        }

        [Fact]
        public void Constructor_ShouldThrowDomainException_WhenNameIsEmpty()
        {
            // Act
            var exception = Assert.Throws<DomainException>(() => new Tag(string.Empty));

            // Assert
            Assert.NotNull(exception);
        }
    }
}
