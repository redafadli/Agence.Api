using Agence.Api.Application.Repositories;
using Agence.Api.Domain.Entities;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Agence.Api.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        public readonly IConfiguration _configuration;
        private Cloudinary cloudinary;

        public ImageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            cloudinary = new Cloudinary(_configuration.GetConnectionString("CLOUDINARY_URL"));
            cloudinary.Api.Secure = true;
        }

        [HttpPost]
        public async Task<IActionResult> uploadImage([FromBody] ImageUrlModel imageData)
        {
            try
            {
                var uploadParams = new ImageUploadParams()
                {

                    File = new FileDescription(@"/Users/redafadli/Desktop/TFE/Screenshots 17:04/test.jpg"),
                    UploadPreset = "ml_default",
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                return await Task.Run(() => new OkResult());
            }
            catch
            {
                return await Task.Run(() => new BadRequestResult());
            }
        }


    }
}

