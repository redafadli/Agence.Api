using System;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Services.Interfaces
{
	public interface IFavoriteService
	{
        Task<IEnumerable<Favorite>> getFavoritesByEmailAsync(string user_email);
        Task<IActionResult> postFavoriteAsync(Favorite favorite);
        Task<IActionResult> deleteFavoriteAsync(int favorite_id);
        Task<Favorite> getFavoriteByIdAsync(int id);
        
    }
}

