using System;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Repositories
{
    public interface IListingRepository
    {
        Task<IEnumerable<Listing>> SearchListingsAsync(string term);
        Task<IEnumerable<Listing>> GetListingsAsync();
        Task<Listing> GetListingByIdAsync(int id);
        Task<IActionResult> PostListingAsync(Listing listing);
        Task<IActionResult> PutListingAsync(Listing listing);
    }
}