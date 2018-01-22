namespace _02.SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Attributes;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Username { get; set; }


        //EF core does not support custom attributes yet
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [ValidPassword]
        public string Password { get; set; }

        [Required]
        [RegularExpression("^[^._-][A-Za-z0-9._-]*[^._-]@\\w+\\.\\w+(?:.\\w+)*$")]
        public string Email { get; set; }

        [MaxLength(1024 * 1024)]
        public byte[] ProfilePicture { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public int? Age { get; set; }

        public bool? IsDeleted { get; set; }

        public ICollection<UserFriend> Followers { get; set; } = new List<UserFriend>();

        public ICollection<UserFriend> Following { get; set; } = new List<UserFriend>();

        public ICollection<UserAlbum> Albums { get; set; } = new List<UserAlbum>();
    }   
}
