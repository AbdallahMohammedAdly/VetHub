using Microsoft.AspNetCore.Mvc;
using VetHub02.Admin.Models;
using VetHub02.Core.Entities;
using VetHub02.Core.Specifications;
using VetHub02.Core.UnitOfWork;

namespace VetHub02.Admin.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ContactUsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(SpecPrams specPrams)
        {
            var spec = new ContactUsSpecification(specPrams);
            var contactUs = await unitOfWork.Repository<ContactUs>().GetAllWithSpecAsync(spec);

           
            var models = new List<ContactUsVeiwModel>();

            foreach (var itemcontactUs in contactUs)
            {
              
                var model = new ContactUsVeiwModel()
                {
                    Id = itemcontactUs.Id,
                    Email = itemcontactUs.Email,
                    Name = itemcontactUs.Name,
                    Message = itemcontactUs.Message,
                    DateOfCreate = itemcontactUs.DateOfCreate,
                    PhoneNumber = itemcontactUs.PhoneNumber,
                    UserId = itemcontactUs.UserId
                };
                models.Add(model); 
            }

            return View(models);
        }
    }
}
