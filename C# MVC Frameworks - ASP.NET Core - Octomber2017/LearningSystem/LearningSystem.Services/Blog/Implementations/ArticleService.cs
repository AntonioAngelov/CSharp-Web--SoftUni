namespace LearningSystem.Services.Blog.Implementations
{
    using AutoMapper.QueryableExtensions;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using System;
    using System.Linq;
    using LearningSystem.Services.Blog.Models;
    using System.Collections.Generic;

    public class ArticleService : IArticleService
    {
        private readonly LearningSystemDbContext db;

        public ArticleService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public void Create(string title, string content, string username)
        {
            var author = this.db.Users.FirstOrDefault(u => u.UserName == username);

            if (author == null)
            {
                return;
            }

            var article = new Article
            {
                Author = author,
                Title = title,
                Content = content,
                PublishedDate = DateTime.UtcNow
            };

            this.db.Articles.Add(article);
            this.db.SaveChanges();
        }

        public IEnumerable<ArticleListingModel> AllListing()
            => this.db.Articles
            .ProjectTo<ArticleListingModel>()
            .ToList();
    }
}
