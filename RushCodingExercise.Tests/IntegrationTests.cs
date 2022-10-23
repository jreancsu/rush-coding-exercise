using RushCodingExercise.Services;

namespace RushCodingExercise.Tests
{
    public class IntegrationTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly PhotoAlbumService _photoAlbumService;
        private readonly IntegrationTestFixture _testFixture;

        public IntegrationTests(IntegrationTestFixture testFixture)
        {
            _testFixture = testFixture;
            _photoAlbumService = new PhotoAlbumService(testFixture.HttpClient);
        }

        [Fact]
        public async Task CanFetchAlbumDetailsById()
        {
            const int albumIdToTest = 4;

            var albumDetails = await _photoAlbumService.GetAlbumDetailsById(albumIdToTest);

            Assert.NotNull(albumDetails);
            Assert.Equal(albumIdToTest, albumDetails!.AlbumId);
            Assert.NotEmpty(albumDetails.PhotoDetails);

            var firstPhotoDetails = albumDetails.PhotoDetails.First();

            Assert.NotNull(firstPhotoDetails.Title);
            Assert.True(firstPhotoDetails.Id != default(int));
            Assert.NotNull(firstPhotoDetails.Url);
            Assert.NotNull(firstPhotoDetails.ThumbnailUrl);
        }

        [Fact]
        public async Task CanFetchAllAlbumDetails()
        {
            var allAlbumDetails = await _photoAlbumService.GetAllAlbumDetails();

            Assert.NotNull(allAlbumDetails);

            var firstAlbumDetails = allAlbumDetails.First();

            Assert.True(firstAlbumDetails.AlbumId != default(int));

            var firstPhotoDetails = firstAlbumDetails.PhotoDetails.First();

            Assert.NotNull(firstPhotoDetails.Title);
            Assert.True(firstPhotoDetails.Id != default(int));
            Assert.NotNull(firstPhotoDetails.Url);
            Assert.NotNull(firstPhotoDetails.ThumbnailUrl);
        }
    }
}
