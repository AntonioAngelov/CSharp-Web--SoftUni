namespace _02.SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Attributes;

    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [Tag]
        public string Title { get; set; }

        public ICollection<AlbumTag> Albums { get; set; } = new List<AlbumTag>();
    }
}
