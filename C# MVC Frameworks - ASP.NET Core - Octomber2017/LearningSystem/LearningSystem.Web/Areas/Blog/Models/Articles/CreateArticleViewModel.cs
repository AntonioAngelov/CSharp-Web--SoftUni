using LearningSystem.Data;

namespace LearningSystem.Web.Areas.Blog.Models.Articles
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class CreateArticleViewModel
    {
        [Required]
        [MaxLength(ArticleTitleMaxLength)]
        [MinLength(ArticleTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ArticleContentMinLength)]
        public string Content { get; set; }
    }
}
