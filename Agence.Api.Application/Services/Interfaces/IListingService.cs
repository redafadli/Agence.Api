﻿using System;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Services.Interfaces
{

    public interface IListingService
    {
        Task<IEnumerable<Listing>> GetListingsAsync();
        Task<Listing> GetListingAsync(int id);
        Task<IActionResult> PostListingAsync(Listing listing);
        Task<IActionResult> PutListingAsync(Listing listing);
        Task<IActionResult> DeleteListingByIdAsync(int id);
    }
}