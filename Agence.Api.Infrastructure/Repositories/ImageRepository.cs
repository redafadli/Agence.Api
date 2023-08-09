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

        public async Task<ImageUrl> uploadImage(ImageUrl imageData)
        {
            var uploadParams = new ImageUploadParams()
            {

                File = new FileDescription("image.jpg", imageData.InputImage),
                UploadPreset = "ml_default",
            };
            var uploadResult = cloudinary.Upload(uploadParams);

            return new ImageUrl { ImageUri = uploadResult.SecureUrl };
        }
    }
}
