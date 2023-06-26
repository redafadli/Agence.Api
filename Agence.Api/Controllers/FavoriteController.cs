using System;
using Agence.Api.Application.Services;
using Agence.Api.Application.Services.Interfaces;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Controllers
{
    [Route("[controller]")]
	[ApiController]

	public class FavoriteController : Controller
	{
		private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService ?? throw new ArgumentNullException(nameof(favoriteService));
        }

        [HttpGet("id/{id}")]
        public async Task<Favorite> GetFavoriteByIdAsync([FromRoute] int id)
        {
            return await _favoriteService.getFavoriteByIdAsync(id);
        }

        [HttpGet("user_email/{user_email}")]
        public async Task<IEnumerable<Favorite>> GetFavoritesByEmailAsync([FromRoute] string user_email)
        {
            return await _favoriteService.getFavoritesByEmailAsync(user_email);
        }

        [HttpPost]
        public async Task<IActionResult> PostFavoriteAsync([FromBody] Favorite favorite)
        {
            return await _favoriteService.postFavoriteAsync(favorite);
        }

        [HttpDelete("delete/{favorite_id}")]
        public async Task<IActionResult> DeleteFavoriteAsync([FromRoute] int favorite_id)
        {
            return await _favoriteService.deleteFavoriteAsync(favorite_id);
        }
    }
}

