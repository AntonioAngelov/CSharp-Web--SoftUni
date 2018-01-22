namespace LearningSystem.Services.Blog
{
    using LearningSystem.Services.Blog.Models;
    using System.Collections.Generic;

    public interface IArticleService
    {
        void Create(string title, string content, string username);

        IEnumerable<ArticleListingModel> AllListing();
    }
}
