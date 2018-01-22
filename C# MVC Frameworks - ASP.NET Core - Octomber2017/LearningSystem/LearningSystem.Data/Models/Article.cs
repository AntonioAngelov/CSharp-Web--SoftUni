namespace LearningSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ArticleTitleMaxLength)]
        [MinLength(ArticleTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ArticleContentMinLength)]
        public string Content { get; set; }

        public DateTime PublishedDate { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
