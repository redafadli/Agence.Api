using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agence.Api.Application.Services;
using Agence.Api.Application.Services.Interfaces;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ListingController : Controller
    {

        private readonly IListingService _listingService;

        public ListingController(IListingService listingService)
        {
            _listingService = listingService ?? throw new ArgumentNullException(nameof(listingService));
        }

        [HttpGet("/")]
        public async Task<IEnumerable<Listing>> GetListingsAsync()
        {
            return await _listingService.GetListingsAsync();
        }

        [HttpGet("{id}")]
        public async Task<Listing> GetListingAsync([FromRoute] int id)
        {
            return await _listingService.GetListingAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostListingAsync([FromBody] Listing listing)
        {
            return await _listingService.PostListingAsync(listing);

        }
        [HttpPut("/editListing/{id}")]
        public async Task<IActionResult> PutListingAsync([FromBody] Listing listing)
        {
            return await _listingService.PutListingAsync(listing);
        }

        [HttpDelete("/deleteListing/{id}")]
        public async Task<IActionResult> DeleteListingByIdAsync([FromRoute]int id)
        {
            return await _listingService.DeleteListingByIdAsync(id);
        }
    }
}