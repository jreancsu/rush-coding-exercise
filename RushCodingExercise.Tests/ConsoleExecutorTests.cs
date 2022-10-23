using Moq;
using RushCodingExercise.Interfaces;

namespace RushCodingExercise.Tests
{
    public class ConsoleExecutorTests
    {
        private readonly Mock<IPhotoAlbumService> _mockPhotoAlbumService;
        private readonly Mock<IConsoleService> _mockConsoleService;

        public ConsoleExecutorTests()
        {
            _mockPhotoAlbumService = new Mock<IPhotoAlbumService>();
            _mockConsoleService = new Mock<IConsoleService>();
        }

        [Theory]
        [InlineData("taco")]
        [InlineData(":-)")]
        [InlineData("all photos")]
        public async Task DoesNotFetchAlbumDetailsForBadInput(string input)
        {
            _mockConsoleService.Setup(x => x.ReadLine()).Returns(input);
            _mockConsoleService.Setup(x => x.ReadKey()).Returns(new ConsoleKeyInfo());

            var consoleExecutor = new ConsoleExecutor(_mockPhotoAlbumService.Object, _mockConsoleService.Object);
            await consoleExecutor.Execute();

            _mockPhotoAlbumService.Verify(x => x.GetAlbumDetailsById(It.IsAny<int>()), Times.Never);
            _mockPhotoAlbumService.Verify(x => x.GetAllAlbumDetails(), Times.Never);
        }

        [Theory]
        [InlineData("all")]
        [InlineData("ALL")]
        [InlineData("aLL")]
        public async Task FetchesAllAlbumDetailsIfInputIsAll(string input)
        {
            _mockConsoleService.Setup(x => x.ReadLine()).Returns(input);
            _mockConsoleService.Setup(x => x.ReadKey()).Returns(new ConsoleKeyInfo());

            var consoleExecutor = new ConsoleExecutor(_mockPhotoAlbumService.Object, _mockConsoleService.Object);
            await consoleExecutor.Execute();

            _mockPhotoAlbumService.Verify(x => x.GetAlbumDetailsById(It.IsAny<int>()), Times.Never);
            _mockPhotoAlbumService.Verify(x => x.GetAllAlbumDetails(), Times.Once);
        }

        [Theory]
        [InlineData("14")]
        [InlineData("3")]
        [InlineData("9")]
        public async Task FetchesAlbumDetailsByIdIfInputIsInteger(string input)
        {
            _mockConsoleService.Setup(x => x.ReadLine()).Returns(input);
            _mockConsoleService.Setup(x => x.ReadKey()).Returns(new ConsoleKeyInfo());

            var consoleExecutor = new ConsoleExecutor(_mockPhotoAlbumService.Object, _mockConsoleService.Object);
            await consoleExecutor.Execute();

            _mockPhotoAlbumService.Verify(x => x.GetAlbumDetailsById(It.Is<int>(x => x == int.Parse(input))), Times.Once);
            _mockPhotoAlbumService.Verify(x => x.GetAllAlbumDetails(), Times.Never);
        }
    }
}
