using RushCodingExercise.Models;

namespace RushCodingExercise.Interfaces
{
    public interface IPhotoAlbumService
    {
        Task<PhotoAlbumDetails?> GetAlbumDetailsById(int albumId);
        Task<IEnumerable<PhotoAlbumDetails>> GetAllAlbumDetails();
    }
}
