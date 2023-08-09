using System;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Services.Interfaces
{
	public interface IImageService
	{
        Task<ImageUrl> uploadImage(ImageUrl imageData);

    }
}

