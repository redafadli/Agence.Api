﻿using System;
using Agence.Api.Application.Repositories;
using Agence.Api.Application.Services.Interfaces;
using Agence.Api.Domain.Entities;

namespace Agence.Api.Application.Services {
    public class ListingService : IListingService {

        private readonly IListingRepository _listingRepository;

        public ListingService(IListingRepository listingRepository) {
            _listingRepository = listingRepository;
        }

        public async Task<IEnumerable<Listing>> SearchListingsAsync(string term) {
            return await _listingRepository.SearchListingsAsync(term);
        }
        public async Task<IEnumerable<Listing>> GetListingsAsync() {
            return await _listingRepository.GetListingsAsync();
        }
        
        public async Task<Listing> GetListingAsync(int id) {
            return await _listingRepository.GetListingAsync(id);
        }

        
    }
}

