using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VetHub02.Core.Entities;
using VetHub02.Core.Entities.identity;
using VetHub02.Core.Repositories;
using VetHub02.Core.Specifications;
using VetHub02.Core.UnitOfWork;
using VetHub02.DTO;
using VetHub02.Helpers;

namespace VetHub02.Controllers
{
  
    public class QuestionController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;

        public QuestionController(IUnitOfWork unitOfWork ,IMapper mapper,UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
        }
   
        [HttpGet("GetAllQuestions")]

        public async Task<ActionResult<Pagination<Question>>> GetAllQuestions([FromQuery]SpecPrams specPrams)
        {
           var sepc = new QuestionSpecification(specPrams);

            var questions = await unitOfWork.Repository<Question>().GetAllWithSpecAsync(sepc);
            // var questions = await repoQuestion.GetAllAsync();
            var data = mapper.Map<IReadOnlyList<QuestionDto>>(questions);

            var CountSpec = new QuestionWithFilterationForCountWithSpec(specPrams);

            var count = await unitOfWork.Repository<Question>().GetCountWithSpecAsync(CountSpec);

            return Ok(new Pagination<QuestionDto>(specPrams.PageIndex, specPrams.PageSize, count, data));
          
        }
    
        [HttpGet("{id}")]

        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var sepc = new QuestionSpecification(id);

            var Question = await unitOfWork.Repository<Question>().GetByIdWithSpecAsync(sepc);

          //  var Question = await repoQuestion.GetByIdAsync(id);

            return Ok(Question);

        }
      
        [HttpPost("PostQuestion")]
        public async Task<ActionResult<Question>> PostQuestion(string email,[FromForm] QuestionDto question)
        {
            var user = await unitOfWork.Repository<User>().GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (question != null)
            {
                var mappedQuestion = mapper.Map<Question>(question);
                mappedQuestion.UserId = user.Id;
                unitOfWork.Repository<Question>().Add(mappedQuestion);
                unitOfWork.Complete();
                return Ok("Question Added Seccussfully");
            }

            return Ok("Try Again");
        }

      
        [HttpDelete("DeleteQuestion")]
        public async Task<ActionResult<Question>> DeleteQuestion(int id, int userId)
        {
            var existQuestion = await unitOfWork.Repository<Question>().GetByIdAsync(id);

            // Check if the question exists
            if (existQuestion == null)
            {
                return NotFound("Question not found");
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
                unitOfWork.Repository<Question>().Delete(existQuestion);
                 unitOfWork.Complete();
                return Ok("Question deleted by Admin");
            }
            else if (existQuestion.UserId == userId)
            {
                unitOfWork.Repository<Question>().Delete(existQuestion);
                 unitOfWork.Complete();
                return Ok("Question deleted by user");
            }
            else
            {
                return Forbid(); // User not authorized to delete the question
            }
        }
    
        //[Authorize(Roles ="Admin")]
        [HttpGet("GetUserQuestions")]
        public async Task<ActionResult<IEnumerable<Question>>> GetUserQuestions(int userId)
        {
            var user = await unitOfWork.Repository<User>().GetByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Assuming User has a collection navigation property called Questions
            var userQuestions = user.Questions;

            return Ok(userQuestions);
        }
   
        [HttpPut("UpdateQuestion")]
        public async Task<ActionResult<Question>> UpdateQuestion(int id, int userId,[FromForm] Question updatedQuestion)
        {
            var existingQuestion = await unitOfWork.Repository<Question>().GetByIdAsync(id);

            if (existingQuestion == null)
            {
                return NotFound("Question not found");
            }

            var user = await unitOfWork.Repository<User>().GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var usertocheck = await userManager.FindByEmailAsync(user.Email);

            var userRoles = await userManager.GetRolesAsync(usertocheck);

            // Ensure that the user updating the question is the owner or an admin
            if (existingQuestion.UserId != userId && !userRoles.Contains("Admin"))
            {
                return Forbid("You are not authorized to update this question");
            }

            existingQuestion.Title = updatedQuestion.Title;
            existingQuestion.Content = updatedQuestion.Content;

            unitOfWork.Repository<Question>().Update(existingQuestion);
             unitOfWork.Complete();

            return Ok(existingQuestion);
        }
    }
    
}
