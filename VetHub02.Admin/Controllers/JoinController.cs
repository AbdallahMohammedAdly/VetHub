using Microsoft.AspNetCore.Mvc;
using VetHub02.Admin.Models;
using VetHub02.Core.Entities;
using VetHub02.Core.Specifications;
using VetHub02.Core.UnitOfWork;
using VetHub02.Repository;

namespace VetHub02.Admin.Controllers
{
    public class JoinController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public JoinController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(SpecPrams specPrams)
        {
            var spec = new JoinSpecification(specPrams);
            var Join = await unitOfWork.Repository<Join>().GetAllWithSpecAsync(spec);


            var models = new List<JoinViewModel>();

            foreach (var itemJoin in Join)
            {

                var model = new JoinViewModel()
                {
                    Id = itemJoin.Id,
                    Email = itemJoin.Email,
                    Name = itemJoin.Name,
                    DateTime = itemJoin.DateTime,
                    Gender = itemJoin.Gender,
                    PhoneNumber = itemJoin.PhoneNumber,
                    CV = itemJoin.CV,
                    DateOfBirth = itemJoin.DateOfBirth,
                    Specialization = itemJoin.Specialization
                };
                models.Add(model);
            }

            return View(models);
        }


    }
}
