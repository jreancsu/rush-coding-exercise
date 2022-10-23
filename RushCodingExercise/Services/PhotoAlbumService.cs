using RushCodingExercise.Interfaces;
using RushCodingExercise.Models;
using System.Text.Json;

namespace RushCodingExercise.Services
{
    public class PhotoAlbumService : IPhotoAlbumService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public PhotoAlbumService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PhotoAlbumDetails?> GetAlbumDetailsById(int albumId)
        {
            var httpResponseContent = await _httpClient.GetStringAsync($"/photos?albumId={albumId}");

            if (string.IsNullOrEmpty(httpResponseContent)) return null;

            var deserialized = JsonSerializer.Deserialize<IEnumerable<PhotoDetails>>(httpResponseContent, _jsonSerializerOptions);

            if (deserialized == null || !deserialized.Any()) return null;

            return new PhotoAlbumDetails
            {
                AlbumId = albumId,
                PhotoDetails = deserialized
            };
        }

        public async Task<IEnumerable<PhotoAlbumDetails>> GetAllAlbumDetails()
        {
            var httpResponseContent = await _httpClient.GetStringAsync($"/photos");

            if (string.IsNullOrEmpty(httpResponseContent)) return Enumerable.Empty<PhotoAlbumDetails>();

            var deserialized = JsonSerializer.Deserialize<IEnumerable<PhotoDetails>>(httpResponseContent, _jsonSerializerOptions);
            if (deserialized == null) return Enumerable.Empty<PhotoAlbumDetails>();

            var grouped = deserialized.GroupBy(x => x.AlbumId);

            return grouped.Select(g => new PhotoAlbumDetails
            {
                AlbumId = g.Key,
                PhotoDetails = g
            });
        }
    }
}
