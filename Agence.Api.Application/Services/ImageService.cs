using System;
using Agence.Api.Application.Repositories;
using Agence.Api.Application.Services.Interfaces;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Services
{
	public class ImageService : IImageService
	{
		private readonly IImageRepository _imageRepository;

		public ImageService(IImageRepository imageRepository)
		{
			_imageRepository = imageRepository;
		}

        public async Task<ImageUrl> uploadImage(ImageUrl imageData)
		{
			return await _imageRepository.UploadImage(imageData);
		}

    }
}

