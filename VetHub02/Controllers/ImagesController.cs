using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetHub02.Core.Repositories;

namespace VetHub02.Controllers
{
    public class ImagesController : BaseApiController
    {
        private readonly IWebHostEnvironment _env;

        public ImagesController(IWebHostEnvironment env)
        {
            _env = env;
        }
  
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var uploadsFolderPath = Path.Combine(_env.WebRootPath, "Images");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var filePath = Path.Combine(uploadsFolderPath, file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Ok(new { filePath });
        }
    
        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var uploadsFolderPath = Path.Combine(_env.WebRootPath, "Images");
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "image/jpeg");
        }
    }
}
