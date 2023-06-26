using System;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Repositories;

public interface IFavoriteRepository
{
	Task<IEnumerable<Favorite>> getFavoritesByEmailAsync(string user_email);
	Task<IActionResult> postFavoriteAsync(Favorite favorite);
	Task<Favorite> getFavoriteByIdAsync(int id);
	Task<IActionResult> deleteFavoriteAsync(int favorite_id);
}

