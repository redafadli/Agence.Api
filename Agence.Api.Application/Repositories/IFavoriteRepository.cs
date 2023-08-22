using System;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Repositories;

public interface IFavoriteRepository
{
	Task<IEnumerable<Favorite>> GetFavoritesByEmailAsync(string user_email);
	Task<IActionResult> PostFavoriteAsync(Favorite favorite);
	Task<Favorite> GetFavoriteByIdAsync(int id);
	Task<IActionResult> DeleteFavoriteAsync(int favorite_id);
}

