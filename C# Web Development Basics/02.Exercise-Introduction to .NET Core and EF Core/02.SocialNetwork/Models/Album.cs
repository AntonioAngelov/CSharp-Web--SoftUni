namespace _02.SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string BackgroundColor { get; set; }

        public bool IsPublic { get; set; }

        public int OwnerId { get; set; }

        public ICollection<UserAlbum> AlbumsHolder { get; set; } = new List<UserAlbum>();

        public ICollection<AlbumPicture> Pictures { get; set; } = new List<AlbumPicture>();

        public ICollection<AlbumTag> Tags { get; set; } = new List<AlbumTag>();
    }
}
