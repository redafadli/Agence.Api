using System;
using Agence.Api.Domain.Entities;

namespace Agence.Api.Application.Services.Interfaces {

    public interface IListingService {
        Task<IEnumerable<Listing>> SearchListingsAsync(string term);
        Task<IEnumerable<Listing>> GetListingsAsync();
    }
}