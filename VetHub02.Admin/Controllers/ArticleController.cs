using Microsoft.AspNetCore.Mvc;
using VetHub02.Admin.Models;
using VetHub02.Core.Entities;
using VetHub02.Core.Specifications;
using VetHub02.Core.UnitOfWork;
using VetHub02.Repository;

namespace VetHub02.Admin.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ArticleController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }



        public async Task<IActionResult> Index(SpecPrams specPrams)
        {
            var spec = new ArticleSpecification(specPrams);
            var articles = await unitOfWork.Repository<Article>().GetAllWithSpecAsync(spec);

            // Create a list to store ArticleViewModel instances
            var models = new List<ArticleViewModel>();

            foreach (var article in articles)
            {
                // Create a new ArticleViewModel instance in each iteration
                var model = new ArticleViewModel()
                {
                    Id = article.Id,
                    Title = article.Title,
                    TimeOfArticle = article.TimeOfArticle,
                    Comments = article.Comments,
                    Content = article.Content,
                    Description = article.Description,
                    UserId = article.UserId,
                };
                models.Add(model); // Add the model to the list
            }

            return View(models); // Pass the list of ArticleViewModel instances to the view
        }

        public async Task<IActionResult> Edit(int id)
        {
            var spec = new ArticleSpecification(id);

            var article = await unitOfWork.Repository<Article>().GetByIdWithSpecAsync(spec);
            if (article == null)
                return NotFound("Article not found");

            
            return View(new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Description= article.Description,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleViewModel articleViewModel)
        {
          //  var user = unitOfWork.Repository<User>().GetUserIdByEmailAsync(email);

            var spec = new ArticleSpecification(articleViewModel.Id);

            var article = await unitOfWork.Repository<Article>().GetByIdWithSpecAsync(spec);
            if (article == null)
                return NotFound("Article not found");

            unitOfWork.Repository<Article>().Update(new Article
            {
                Title = article.Title,
                Description = article.Description,
                Content = article.Content,
                UserId = article.Id,
            });

            unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }
    }
}

