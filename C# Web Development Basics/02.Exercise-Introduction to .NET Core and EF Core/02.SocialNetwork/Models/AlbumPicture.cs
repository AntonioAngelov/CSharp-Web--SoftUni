namespace _02.SocialNetwork.Models
{
    public class AlbumPicture
    {
        public int AlbumId { get; set; }

        public Album Album { get; set; }

        public int PictureId { get; set; }

        public Picture Picture { get; set; }
    }
}
