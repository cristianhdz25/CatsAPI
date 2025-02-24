using CatAPI.BC.Models;
using CatAPI.BW.Interfaces.DA;
using CatAPI.BW.UC;
using Moq;


namespace CatAPI.Tests
{
    public class ManageCatBWTests
    {
        private readonly Mock<IManageCatDA> _mockManageCatDA;
        private readonly ManageCatBW _manageCatBW;

        public ManageCatBWTests()
        {
            _mockManageCatDA = new Mock<IManageCatDA>();
            _manageCatBW = new ManageCatBW(_mockManageCatDA.Object);
        }

        // Prueba para RegisterCatBreedAsync
        [Fact]
        public async Task RegisterCatBreedAsync_ShouldReturnSuccess_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var catBreed = new CatBreed { Name = "Siamese" };
            _mockManageCatDA
                .Setup(da => da.RegisterCatBreedAsync(It.IsAny<CatBreed>()))
                .ReturnsAsync(true);

            // Act
            var result = await _manageCatBW.RegisterCatBreedAsync(catBreed);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Cat breed registered successfully", result.Message);
        }

        [Fact]
        public async Task RegisterCatBreedAsync_ShouldReturnFailure_WhenBreedAlreadyExists()
        {
            // Arrange
            var catBreed = new CatBreed { Name = "Persian" };
            _mockManageCatDA
                .Setup(da => da.RegisterCatBreedAsync(It.IsAny<CatBreed>()))
                .ReturnsAsync(false);

            // Act
            var result = await _manageCatBW.RegisterCatBreedAsync(catBreed);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Cat breed name already exists", result.Message);
        }

        // Prueba para GetCatBreedsAsync
        [Fact]
        public async Task GetCatBreedsAsync_ShouldReturnPaginatedResult()
        {
            // Arrange
            var catBreeds = new List<CatBreed>
            {
                new CatBreed { IdCatBreed = 1, Name = "Siamese" },
                new CatBreed { IdCatBreed = 2, Name = "Persian" }
            };
            var paginatedResult = new PaginatedResult<CatBreed>
            {
                Items = catBreeds,
                TotalCount = 2
            };
            _mockManageCatDA
                .Setup(da => da.GetCatBreedsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(paginatedResult);

            // Act
            var result = await _manageCatBW.GetCatBreedsAsync(1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Items.Count);
        }

        // Prueba para GetCatBreedByIdAsync
        [Fact]
        public async Task GetCatBreedByIdAsync_ShouldReturnCatBreed()
        {
            // Arrange
            var catBreed = new CatBreed { IdCatBreed = 1, Name = "Siamese" };
            _mockManageCatDA
                .Setup(da => da.GetCatBreedByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(catBreed);

            // Act
            var result = await _manageCatBW.GetCatBreedByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Siamese", result.Name);
        }

        // Prueba para UpdateCatBreedAsync
        [Fact]
        public async Task UpdateCatBreedAsync_ShouldReturnSuccess_WhenUpdateIsSuccessful()
        {
            // Arrange
            var catBreed = new CatBreed { IdCatBreed = 1, Name = "Siamese" };
            _mockManageCatDA
                .Setup(da => da.UpdateCatBreedAsync(It.IsAny<CatBreed>()))
                .ReturnsAsync(true);

            // Act
            var result = await _manageCatBW.UpdateCatBreedAsync(catBreed);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Cat breed updated successfully", result.Message);
        }

        [Fact]
        public async Task UpdateCatBreedAsync_ShouldReturnFailure_WhenUpdateFails()
        {
            // Arrange
            var catBreed = new CatBreed { IdCatBreed = 1, Name = "Siamese" };
            _mockManageCatDA
                .Setup(da => da.UpdateCatBreedAsync(It.IsAny<CatBreed>()))
                .ReturnsAsync(false);

            // Act
            var result = await _manageCatBW.UpdateCatBreedAsync(catBreed);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Error updating cat breed", result.Message);
        }

        // Prueba para DeleteCatBreedAsync
        [Fact]
        public async Task DeleteCatBreedAsync_ShouldReturnSuccess_WhenDeletionIsSuccessful()
        {
            // Arrange
            _mockManageCatDA
                .Setup(da => da.DeleteCatBreedAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _manageCatBW.DeleteCatBreedAsync(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Cat breed deleted successfully", result.Message);
        }

        [Fact]
        public async Task DeleteCatBreedAsync_ShouldReturnFailure_WhenDeletionFails()
        {
            // Arrange
            _mockManageCatDA
                .Setup(da => da.DeleteCatBreedAsync(It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            var result = await _manageCatBW.DeleteCatBreedAsync(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Error deleting cat breed", result.Message);
        }
    }
}