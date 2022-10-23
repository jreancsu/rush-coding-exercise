using RichardSzalay.MockHttp;
using RushCodingExercise.Services;
using System.Text.Json;

namespace RushCodingExercise.Tests
{
    public class PhotoAlbumServiceTests
    {
        private readonly MockHttpMessageHandler _mockHttpMessageHandler;
        private readonly string _mockPhotoEndpointBaseAddress = "https://some.fakephotoservice.net";

        public PhotoAlbumServiceTests()
        {
            _mockHttpMessageHandler = new MockHttpMessageHandler();
        }

        [Fact]
        public async Task CanGetAlbumDetailsById()
        {
            const int mockAlbumId = 4;
            var mockResponse = new[]
            {
                new {
                    albumId = mockAlbumId,
                    id = 204,
                    title = "Mona Lisa",
                    url = "https://somefakeurl.com/204",
                    thumbnailUrl = "https://somefakeurl.com/204/thumbnail"
                },
                new {
                    albumId = mockAlbumId,
                    id = 205,
                    title = "Starry Night",
                    url = "https://somefakeurl.com/205",
                    thumbnailUrl = "https://somefakeurl.com/205/thumbnail"
                }
            };
            var serializedMockResponse = JsonSerializer.Serialize(mockResponse);

            _mockHttpMessageHandler
                .When($"{_mockPhotoEndpointBaseAddress}/photos?albumId={mockAlbumId}")
                .Respond("application/json", serializedMockResponse);

            var photoAlbumService = new PhotoAlbumService(new HttpClient(_mockHttpMessageHandler)
            {
                BaseAddress = new Uri(_mockPhotoEndpointBaseAddress)
            });

            var response = await photoAlbumService.GetAlbumDetailsById(mockAlbumId);

            Assert.NotNull(response);
            Assert.Equal(mockAlbumId, response!.AlbumId);
            Assert.Equal(2, response.PhotoDetails.Count());
            Assert.Single(response.PhotoDetails, pd =>
                pd.Id == 204 && pd.Title == "Mona Lisa" &&
                pd.Url == new Uri("https://somefakeurl.com/204") &&
                pd.ThumbnailUrl == new Uri("https://somefakeurl.com/204/thumbnail"));
        }

        [Fact]
        public async Task GetAlbumDetailsById_ReturnsNullForEmptyHttpResponse()
        {
            const int mockAlbumId = 4;
            var mockResponse = Enumerable.Empty<object>();
            var serializedMockResponse = JsonSerializer.Serialize(mockResponse);

            _mockHttpMessageHandler
                .When($"{_mockPhotoEndpointBaseAddress}/photos?albumId={mockAlbumId}")
                .Respond("application/json", serializedMockResponse);

            var photoAlbumService = new PhotoAlbumService(new HttpClient(_mockHttpMessageHandler)
            {
                BaseAddress = new Uri(_mockPhotoEndpointBaseAddress)
            });

            var response = await photoAlbumService.GetAlbumDetailsById(mockAlbumId);

            Assert.Null(response);
        }

        [Fact]
        public async Task CanGetAllAlbumDetails()
        {
            const int mockAlbumId1 = 4;
            const int mockAlbumId2 = 14;
            var mockResponse = new[]
            {
                new {
                    albumId = mockAlbumId1,
                    id = 204,
                    title = "Mona Lisa",
                    url = "https://somefakeurl.com/204",
                    thumbnailUrl = "https://somefakeurl.com/204/thumbnail"
                },
                new {
                    albumId = mockAlbumId1,
                    id = 205,
                    title = "Starry Night",
                    url = "https://somefakeurl.com/205",
                    thumbnailUrl = "https://somefakeurl.com/205/thumbnail"
                },
                new {
                    albumId = mockAlbumId2,
                    id = 205,
                    title = "Driver's License",
                    url = "https://somefakeurl.com/429",
                    thumbnailUrl = "https://somefakeurl.com/429/thumbnail"
                }
            };
            var serializedMockResponse = JsonSerializer.Serialize(mockResponse);

            _mockHttpMessageHandler
                .When($"{_mockPhotoEndpointBaseAddress}/photos")
                .Respond("application/json", serializedMockResponse);

            var photoAlbumService = new PhotoAlbumService(new HttpClient(_mockHttpMessageHandler)
            {
                BaseAddress = new Uri(_mockPhotoEndpointBaseAddress)
            });

            var response = await photoAlbumService.GetAllAlbumDetails();

            Assert.NotNull(response);
            Assert.Equal(2, response.Count());
            Assert.Single(response, albumDetails =>
                albumDetails.AlbumId == mockAlbumId1 && albumDetails.PhotoDetails.Count() == 2);
            Assert.Single(response, albumDetails =>
                albumDetails.AlbumId == mockAlbumId2 && albumDetails.PhotoDetails.Count() == 1);
        }

        [Fact]
        public async Task GetAllAlbumDetails_ReturnsEmptyCollectionForEmptyHttpResponse()
        {
            const int mockAlbumId = 13;
            var mockResponse = Enumerable.Empty<object>();
            var serializedMockResponse = JsonSerializer.Serialize(mockResponse);

            _mockHttpMessageHandler
                .When($"{_mockPhotoEndpointBaseAddress}/photos")
                .Respond("application/json", serializedMockResponse);

            var photoAlbumService = new PhotoAlbumService(new HttpClient(_mockHttpMessageHandler)
            {
                BaseAddress = new Uri(_mockPhotoEndpointBaseAddress)
            });

            var response = await photoAlbumService.GetAllAlbumDetails();

            Assert.Empty(response);
        }
    }
}