﻿namespace _02.SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Caption { get; set; }

        [Required]
        public string Path { get; set; }

        public ICollection<AlbumPicture> Albums { get; set; } = new List<AlbumPicture>();
    }
}