using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VetHub02.Core.Entities;
using VetHub02.Core.Entities.identity;
using VetHub02.Core.Specifications;
using VetHub02.Core.UnitOfWork;
using VetHub02.DTO;
using VetHub02.Errors;
using VetHub02.Helpers;

namespace VetHub02.Controllers
{
    public class HistoryController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;

        public HistoryController(IUnitOfWork unitOfWork ,IMapper mapper ,UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet("GetAllHistory")]
        public async Task<ActionResult<IReadOnlyList<HistoryDto>>> GetAllHistory(int userId, [FromQuery] SpecPrams specPrams)
        {
            
            var spec = new HistorySpecification(specPrams);

           
            var historyRecords = await unitOfWork.Repository<History>().GetAllWithSpecAsync(spec);

            
            var userHistoryRecords = historyRecords.Where(h => h.UserId == userId).ToList();

            
            var data = mapper.Map<IReadOnlyList<HistoryDto>>(userHistoryRecords);

            return Ok(data);
        }


        [HttpPost("AddHistory")]

        public  ActionResult AddHistory( [FromForm] HistoryDto historyDto)
        {
         
            if (historyDto != null)
            {
                var history = mapper.Map<History>(historyDto);
                unitOfWork.Repository<History>().Add(history);
                unitOfWork.Complete();
                return Ok("History Added Seccussfully");
            }

            return Ok("try Again.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArticle(int id, [FromQuery] int userId)
        {
            var existHistory = await unitOfWork.Repository<History>().GetByIdAsync(id);

            // Check if the history record exists
            if (existHistory == null)
            {
                return NotFound("History not found");
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
                unitOfWork.Repository<History>().Delete(existHistory);
                 unitOfWork.Complete();
                return Ok("History deleted by Admin");
            }
            // Check if the user is the owner of the history record
            else if (existHistory.UserId == userId)
            {
                unitOfWork.Repository<History>().Delete(existHistory);
                 unitOfWork.Complete();
                return Ok("History deleted by user");
            }
            else
            {
                return Forbid(); // User not authorized to delete the history record
            }
        }

    }
}
