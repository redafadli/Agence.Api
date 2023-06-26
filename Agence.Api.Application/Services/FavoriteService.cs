using System;
using Agence.Api.Application.Repositories;
using Agence.Api.Application.Services.Interfaces;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Services
{
	public class FavoriteService : IFavoriteService
	{
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public Task<IActionResult> deleteFavoriteAsync(int favorite_id)
        {
            return _favoriteRepository.deleteFavoriteAsync(favorite_id);
        }

        public Task<Favorite> getFavoriteByIdAsync(int id)
        {
            return _favoriteRepository.getFavoriteByIdAsync(id);
        }

        public Task<IEnumerable<Favorite>> getFavoritesByEmailAsync(string user_email)
        {
            return _favoriteRepository.getFavoritesByEmailAsync(user_email);
        }

        public Task<IActionResult> postFavoriteAsync(Favorite favorite)
        {
            return _favoriteRepository.postFavoriteAsync(favorite);
        }
    }
}

