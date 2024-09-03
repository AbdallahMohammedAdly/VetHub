using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using VetHub02.Core.Entities;
using VetHub02.Core.Entities.identity;
using VetHub02.Core.Repositories;
using VetHub02.Core.Specifications;
using VetHub02.Core.UnitOfWork;
using VetHub02.DTO;
using VetHub02.Repository;

namespace VetHub02.Controllers
{
   
    public class CommentController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;

        public CommentController(IUnitOfWork unitOfWork ,IMapper mapper ,UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
        }
    
        [HttpGet("GetAllComment")]

        public async Task<ActionResult<IReadOnlyList<Comment>>> GetAllComment() 
        {
            var sepc = new CommentSpecification();

            var comments = await unitOfWork.Repository<Comment>().GetAllWithSpecAsync(sepc);
            //var comments = await repoComment.GetAllAsync();
            var mappedComments = mapper.Map<IReadOnlyList<CommentDto>>(comments);
            return Ok(mappedComments);

        }
   
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var sepc = new CommentSpecification(id);

            var comment = await unitOfWork.Repository<Comment>().GetByIdWithSpecAsync(sepc);

            // var comment = await repoComment.GetByIdAsync(id);
            var mappedComment = mapper.Map<CommentDto>(comment);
            return Ok(mappedComment);
          

        }

 
        [HttpPost("PostComment")]

        public async Task<ActionResult<Comment>> PostComment([FromForm] CommentDto comment ,string email)
        {
            var user = await unitOfWork.Repository<User>().GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (comment != null)
            {
                var mappedComment = mapper.Map<Comment>(comment);
                mappedComment.UserId = user.Id;
                unitOfWork.Repository<Comment>().Add(mappedComment);
                unitOfWork.Complete();
                return Ok("Comment Added Seccussfully");
            }
          
            return Ok("Try Again");
        }
        [HttpPost("article/{articleId}")]
        public async Task<IActionResult> PostCommentOnArticle(int articleId, [FromBody] CommentDto commentDto)
        {
            // Call service method to post comment on article
            var article = await  unitOfWork.Repository<Article>().GetByIdAsync(articleId);
            if (article == null)
                return NotFound("Article not found");

            var mappedComment = mapper.Map<Comment>(commentDto);

            // var result =
            mappedComment.ArticleId = articleId; 
            unitOfWork.Repository<Comment>().Add( mappedComment);
            
           unitOfWork.Complete();


            return Ok();
        }
  
        // POST: api/comments/question/{questionId}
        [HttpPost("question/{questionId}")]
        public async Task<IActionResult> PostCommentOnQuestion(int questionId, [FromBody] CommentDto commentDto)
        {
            // Call service method to post comment on question

            var question = await unitOfWork.Repository<Question>().GetByIdAsync(questionId);
            if (question == null)
                return NotFound("question not found");

            var mappedComment = mapper.Map<Comment>(commentDto);

            // var result =
            mappedComment.ArticleId = questionId;
            unitOfWork.Repository<Comment>().Add(mappedComment);

            unitOfWork.Complete();



            return BadRequest();
        }

        [HttpDelete("DeleteComment")]
        public async Task<ActionResult<Comment>> DeleteComment(int id, int userId)
        {
            var existComment = await unitOfWork.Repository<Comment>().GetByIdAsync(id);

            // Check if the comment exists
            if (existComment == null)
            {
                return NotFound("Comment not found");
            }

            var userTryDelete = await unitOfWork.Repository<User>().GetByIdAsync(userId);
            if (userTryDelete == null)
            {
                return NotFound("User not found");
            }

            var userToCheck = await userManager.FindByEmailAsync(userTryDelete.Email);
            if (userToCheck == null)
            {
                return NotFound("User not found");
            }

            var userRoles = await userManager.GetRolesAsync(userToCheck);

            // Check if the user has admin role
            if (userRoles.Contains("Admin"))
            {
                unitOfWork.Repository<Comment>().Delete(existComment);
                 unitOfWork.Complete();
                return Ok("Comment deleted by Admin");
            }
            else if (existComment.UserId == userId)
            {
                unitOfWork.Repository<Comment>().Delete(existComment);
                 unitOfWork.Complete();
                return Ok("Comment deleted by user");
            }
            else
            {
                return Forbid(); // User not authorized to delete the comment
            }
        }
      
        [HttpGet("GetUserComments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetUserComments(int userId)
        {
            var user = await unitOfWork.Repository<User>().GetByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Assuming User has a collection navigation property called Comments
            var userComments = user.Comments;

            return Ok(userComments);
        }
     
        [HttpPut("UpdateComment")]
        public async Task<ActionResult<Comment>> UpdateComment(int id, int userId,[FromForm] Comment updatedComment)
        {
            var existingComment = await unitOfWork.Repository<Comment>().GetByIdAsync(id);

            if (existingComment == null)
            {
                return NotFound("Comment not found");
            }

            var user = await unitOfWork.Repository<User>().GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var usertocheck = await userManager.FindByEmailAsync(user.Email);

            var userRoles = await userManager.GetRolesAsync(usertocheck);

            // Ensure that the user updating the comment is the owner or an admin
            if (existingComment.UserId != userId && !userRoles.Contains("Admin"))
            {
                return Forbid("You are not authorized to update this comment");
            }

            existingComment.Content = updatedComment.Content;

            unitOfWork.Repository<Comment>().Update(existingComment);
             unitOfWork.Complete();

            return Ok(existingComment);
        }


    }
}
