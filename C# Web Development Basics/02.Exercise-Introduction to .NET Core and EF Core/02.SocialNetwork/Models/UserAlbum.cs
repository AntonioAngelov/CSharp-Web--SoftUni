namespace _02.SocialNetwork.Models
{
    using Enums;

    public class UserAlbum
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int AlbumId { get; set; }

        public Album Album { get; set; }

        public Role UserRole { get; set; }
    }
}
