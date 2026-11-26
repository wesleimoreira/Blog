using Blog.Domain.Common;
using Blog.Domain.Entities;

namespace Blog.Domain.Tests.Entities
{
    public class CategoryTests
    {
        [Fact]
        public void Constructor_ShouldCreateCategory_WhenDataIsValid()
        {
            // Arrange
            var name = "Technology";
            var description = "Technology articles";

            // Act
            var category = new Category(name, description);

            // Assert
            Assert.Equal(name, category.Name);
            Assert.Equal(description, category.Description);
            Assert.Equal("technology", category.Slug);
            Assert.NotEqual(Guid.Empty, category.Id);
        }

        [Fact]
        public void Update_ShouldUpdateCategory_WhenDataIsValid()
        {
            // Arrange
            var category = new Category("Technology", "Old description");

            var newName = "Programming";
            var newDescription = "Programming articles";

            // Act
            category.Update(newName, newDescription);

            // Assert
            Assert.Equal(newName, category.Name);
            Assert.Equal("programming", category.Slug);
            Assert.Equal(newDescription, category.Description);
            Assert.NotNull(category.UpdatedAt);
        }

        [Fact]
        public void Constructor_ShouldThrowDomainException_WhenNameIsEmpty()
        {
            // Arrange
            var name = string.Empty;
            var description = "Some description";

            // Act
            var exception = Assert.Throws<DomainException>(
                () => new Category(name, description));

            // Assert
            Assert.NotNull(exception);
        }
    }
}
