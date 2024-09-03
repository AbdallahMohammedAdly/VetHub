using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using VetHub02.Core.Entities;
using VetHub02.Core.Entities.identity;
using VetHub02.Core.Repositories;
using VetHub02.Core.Specifications;
using VetHub02.Core.UnitOfWork;
using VetHub02.DTO;
using VetHub02.Helpers;

namespace VetHub02.Controllers
{

    public class ArticleController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;


        public ArticleController(IUnitOfWork unitOfWork, IMapper mapper,
            UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;

        }
     
        [HttpGet("GetAllArticle")]
        public async Task<ActionResult<Pagination<ArticleDto>>> GetAllArticle([FromQuery] SpecPrams SpecPrams)
        {

            var spec = new ArticleSpecification(SpecPrams);

            var articles = await unitOfWork.Repository<Article>().GetAllWithSpecAsync(spec);
            

            var data = mapper.Map<IReadOnlyList<ArticleDto>>(articles);

            var CountSpec = new ArticleWithFilterationForCountwithSpec(SpecPrams);
            var count = await unitOfWork.Repository<Article>().GetCountWithSpecAsync(CountSpec);
            return Ok(new Pagination<ArticleDto>(SpecPrams.PageIndex, SpecPrams.PageSize,count,data) );
        }
   
        [HttpPost("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {

            var spec = new ArticleSpecification(id);

            var article = await unitOfWork.Repository<Article>().GetByIdWithSpecAsync(spec);

            var mappedarticle = mapper.Map<ArticleDto>(article);

            //  var article = await articleRepo.GetByIdAsync(id);

            return Ok(mappedarticle);
        }

        [HttpPost("AddArticle")]
        public  ActionResult AddArticle([FromForm] ArticleDto articleDto)
        {

            if (articleDto != null)
            {
                var mappedArticle = mapper.Map<Article>(articleDto);
                unitOfWork.Repository<Article>().Add(mappedArticle);
                unitOfWork.Complete();
                return Ok("Article Added Seccussfully");
            }

            return Ok("try Again.");
        }
   
        [HttpDelete("{id}")]
        public async Task<ActionResult<Article>> DeleteArticle(int id, [FromBody] int userId)
        {
            var existArticle = await unitOfWork.Repository<Article>().GetByIdAsync(id);

            // Check if the article exists
            if (existArticle == null)
            {
                return NotFound("Article not found");
            }

            var usertryDelete = await unitOfWork.Repository<User>().GetByIdAsync(userId);
            if (usertryDelete == null)
            {
                return NotFound("User not found");
            }

            var usertocheck = await userManager.FindByEmailAsync(usertryDelete.Email);
            if (usertocheck == null)
            {
                return NotFound("User not found");
            }

            var userRoles = await userManager.GetRolesAsync(usertocheck);

            // Check if the user has admin role
            if (userRoles.Contains("Admin"))
            {
                unitOfWork.Repository<Article>().Delete(existArticle);
                unitOfWork.Complete();
                return Ok("Article deleted by Admin");
            }
            else if (existArticle.UserId == userId)
            {
                unitOfWork.Repository<Article>().Delete(existArticle);
                unitOfWork.Complete();
                return Ok("Article deleted by user");
            }
            else
            {
                return Forbid(); // User not authorized to delete the article
            }
        }


        [HttpGet("GetUserArticles")]
        public async Task<ActionResult<IEnumerable<Article>>> GetUserArticles(int userId)
        {
            var user = await unitOfWork.Repository<User>().GetByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Assuming User has a collection navigation property called Articles
            var userArticles = user.Articles;

            return Ok(userArticles);
        }

 
        [HttpPut("UpdateArticle")]
        public async Task<ActionResult<Article>> UpdateArticle(int id, int userId, [FromForm] Article updatedArticle)
        {
            var existingArticle = await unitOfWork.Repository<Article>().GetByIdAsync(id);

            if (existingArticle == null)
            {
                return NotFound("Article not found");
            }

            var user = await unitOfWork.Repository<User>().GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var usertocheck = await userManager.FindByEmailAsync(user.Email);
            
            var userRoles = await userManager.GetRolesAsync(usertocheck);

            // Ensure that the user updating the article is the owner or an admin
            if (existingArticle.UserId != userId && !userRoles.Contains("Admin"))
            {
                return Forbid("You are not authorized to update this article");
            }

            existingArticle.Title = updatedArticle.Title;
            existingArticle.Content = updatedArticle.Content;

            unitOfWork.Repository<Article>().Update(existingArticle);
             unitOfWork.Complete();

            return Ok(existingArticle);
        }


    }
}
