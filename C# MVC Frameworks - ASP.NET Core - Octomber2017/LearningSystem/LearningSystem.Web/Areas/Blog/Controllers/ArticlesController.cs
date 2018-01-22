namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using LearningSystem.Services.Blog;
    using LearningSystem.Web.Areas.Blog.Models.Articles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Blog")]
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly IArticleService articles;

        public ArticlesController(IArticleService articles)
        {
            this.articles = articles;
        }

        [Authorize(Roles = WebConstants.BlogAuthor)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = WebConstants.BlogAuthor)]
        public IActionResult Create(CreateArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.articles.Create(model.Title, model.Content, User.Identity.Name);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All() => View(this.articles.AllListing());
    }
}
