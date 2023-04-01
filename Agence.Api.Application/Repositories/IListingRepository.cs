using System;
using Agence.Api.Domain.Entities;

namespace Agence.Api.Application.Repositories {
    public interface IListingRepository {
        Task<IEnumerable<Listing>> SearchListingsAsync(string term);
    }
}

