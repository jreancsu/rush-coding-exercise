using RushCodingExercise.Interfaces;
using RushCodingExercise.Models;

namespace RushCodingExercise
{
    public class ConsoleExecutor
    {
        private readonly IPhotoAlbumService _photoAlbumService;
        private readonly IConsoleService _consoleService;

        public ConsoleExecutor(IPhotoAlbumService photoAlbumService, IConsoleService consoleService)
        {
            _photoAlbumService = photoAlbumService;
            _consoleService = consoleService;
        }

        public async Task Execute()
        {
            _consoleService.WriteLine("Enter an integer value to fetch photo album information by ID, or type 'All' to fetch all albums:");
            var consoleInput = _consoleService.ReadLine();

            if (string.Equals("all", consoleInput, StringComparison.CurrentCultureIgnoreCase))
            {
                var resp = await _photoAlbumService.GetAllAlbumDetails();
                foreach (var albumDetails in resp)
                {
                    PrintAlbumDetails(albumDetails);
                }
            }
            else if (int.TryParse(consoleInput, out var albumId))
            {
                var resp = await _photoAlbumService.GetAlbumDetailsById(albumId);
                if (resp != null)
                    PrintAlbumDetails(resp);
                else
                    _consoleService.WriteLine($"Could not find album {albumId}");
            }
            else
                _consoleService.WriteLine("Input is invalid. Please enter an integer album ID or 'All'.");

            _consoleService.WriteLine("Press any key to close this window...");
            _consoleService.ReadKey();
        }

        private void PrintAlbumDetails(PhotoAlbumDetails photoAlbumDetails)
        {
            _consoleService.WriteLine($"photo-album {photoAlbumDetails.AlbumId} {string.Join(" ", photoAlbumDetails.PhotoDetails.Select(GetPhotoDetailsString))}{Environment.NewLine}");
        }

        private static string GetPhotoDetailsString(PhotoDetails photoDetails)
        {
            return $"[{photoDetails.Id}] {photoDetails.Title}";
        }
    }
}
