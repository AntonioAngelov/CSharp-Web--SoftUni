namespace LearningSystem.Services.Blog.Models
{
    using LearningSystem.Common.Mapping;
    using LearningSystem.Data.Models;
    using System;
    using AutoMapper;

    public class ArticleListingModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Article, ArticleListingModel>()
                .ForMember(alm => alm.Author, cfg => cfg.MapFrom(a => a.Author.UserName));
        }
    }
}
