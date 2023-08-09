using System;
using Agence.Api.Application.Repositories;
using Agence.Api.Application.Services.Interfaces;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Services {
    public class ListingService : IListingService {

        private readonly IListingRepository _listingRepository;

        public ListingService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public async Task<IActionResult> DeleteListingByIdAsync(int id)
        {
            return await _listingRepository.DeleteListingByIdAsync(id);
        }

        public async Task<IEnumerable<Listing>> GetListingsAsync()
        {
            return await _listingRepository.GetListingsAsync();
        }

        public async Task<Listing> GetListingAsync(int id)
        {
            return await _listingRepository.GetListingByIdAsync(id);
        }

        public async Task<IActionResult> PostListingAsync(Listing listing)
        {
            return await _listingRepository.PostListingAsync(listing);
        }

        public async Task<IActionResult> PutListingAsync(Listing listing)
        {
            return await _listingRepository.PutListingAsync(listing);
        }
    }
}
