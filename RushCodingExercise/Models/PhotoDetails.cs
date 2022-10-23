namespace RushCodingExercise.Models
{
    public class PhotoDetails
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Uri? Url { get; set; }
        public Uri? ThumbnailUrl { get; set; }


        public int AlbumId { get; set; }
    }
}
