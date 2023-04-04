using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agence.Api.Application.Services;
using Agence.Api.Application.Services.Interfaces;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class ListingController : Controller {

        private readonly IListingService _listingService;

        public ListingController(IListingService listingService) {
            _listingService = listingService ?? throw new ArgumentNullException(nameof(listingService));
        }

        [HttpGet("")]
        public async Task<IEnumerable<Listing>> GetListingsAsync() {
            return await _listingService.GetListingsAsync();
        }

        [HttpGet("search/{term}")]
        public async Task<IEnumerable<Listing>> SearchListingsAsync([FromRoute] string term) {
            return await _listingService.SearchListingsAsync(term);
        }
    }
}

