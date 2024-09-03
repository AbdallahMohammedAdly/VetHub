using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetHub02.Core.Entities;
using VetHub02.Core.Specifications;
using VetHub02.Core.UnitOfWork;
using VetHub02.DTO;
using VetHub02.Errors;

namespace VetHub02.Controllers
{
    public class ContactUsController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ContactUsController(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("GetAllContactUs")]

        public async Task<ActionResult> GetAllContactUs([FromForm]SpecPrams specPrams)
        {
            var spec = new ContactUsSpecification(specPrams);

            var contactUs = await unitOfWork.Repository<ContactUs>().GetAllWithSpecAsync(spec);

            if (contactUs != null)
            {
                var contactUsDto = mapper.Map<IReadOnlyList<ContactUsDto>>(contactUs);
                return Ok(contactUsDto);
            }



            return Ok("no data Exist");
        }
        [HttpPost("AddContactUs")]

        public ActionResult AddContactUs(int userId, [FromForm] ContactUsDto contactUsDto)
        {


            if (contactUsDto != null)
            {
                var contactUs = mapper.Map<ContactUs>(contactUsDto);
                contactUs.UserId = userId;
                unitOfWork.Repository<ContactUs>().Add(contactUs);
                unitOfWork.Complete();
                return Ok("ContactUs Added Seccussfully");
            }

            return Ok("try Again.");
        }
    }
}
