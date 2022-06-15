namespace TypingCodeBackEnd.Models
{
    public class Photo
    {
        public long AlbumId { get; set; }
        public long Id { get; set; }
        public string? Title { get; set; }
        public Uri? Url { get; set; }
        public Uri? ThumbnailUrl { get; set; }
    }
}
