using Agence.Api.Application.Services.Interfaces;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Agence.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : Controller
    {

        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("/upload")]
        public async Task<IActionResult> uploadImage([FromBody] ImageUrlModel imageData)
        {
            return await _imageService.uploadImage(imageData);
        }
    }
}

