using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using VetHub02.Core.Entities;
using VetHub02.Core.Entities.identity;
using VetHub02.Core.Servises;
using VetHub02.Core.Specifications;
using VetHub02.Core.UnitOfWork;
using VetHub02.DTO;
using VetHub02.Helpers;

namespace VetHub02.Controllers
{
    public class JoinController : BaseApiController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IMailSettings mailSettings;

        public JoinController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IMailSettings mailSettings)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.mailSettings = mailSettings;
        }
        [HttpPost("JoinUs")]
        public  ActionResult JoinUs(int userId,[FromForm] JoinDto joinDto)
        {

            if (joinDto != null)
            {
                var Join = mapper.Map<Join>(joinDto);
                Join.UserId = userId;
                unitOfWork.Repository<Join>().Add(Join);
                unitOfWork.Complete();
                return Ok("JoinUs Added Seccussfully");
            }

            return Ok("try Again.");
        }

    }
}
