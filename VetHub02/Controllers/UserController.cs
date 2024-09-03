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

namespace VetHub02.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;

        public UserController(IUnitOfWork unitOfWork,IMapper mapper ,UserManager<AppUser> userManager  )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
          
        }
     
        //[Authorize(Roles ="Admin")]
        [HttpGet]
        //[ProducesResponseType(typeof(User),StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<User>>> GetAllUsers()
        {
            var spec = new UserSpecification();
             var users = await unitOfWork.Repository<User>().GetAllWithSpecAsync(spec);
           // var users = await userRepo.GetAllAsync();

            var mappedUser = mapper.Map<IReadOnlyList<UserDto>>(users);
                
            
            return Ok(mappedUser);
        }
   
        [HttpGet("GetUserByEmail")]
        public async Task<ActionResult<int>> GetUserByEmail(string email)
        {

        var user =  await  unitOfWork.Repository<User>().GetUserByEmailAsync(email);

            return Ok(user.Id);
        }
      
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var spec = new UserSpecification(id); 
            var user = await unitOfWork.Repository<User>().GetByIdWithSpecAsync(spec);
           // var user = await userRepo.GetByIdAsync(id);
            return Ok(user);
        }
      
        //[Authorize(Roles ="Admin")]
        [HttpDelete]

        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var spec = new UserSpecification(id);
            var existUser = await unitOfWork.Repository<User>().GetByIdWithSpecAsync(spec);
            if (existUser is not null)
            {
                unitOfWork.Repository<User>().Delete(existUser);
            }
            return Ok(existUser);
        }

    }
}
