using System;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Repositories
{
	public interface IImageRepository
	{
        Task<ImageUrl> uploadImage(ImageUrl imageData);

    }
}

