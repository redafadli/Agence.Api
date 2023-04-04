using System;
using Agence.Api.Application.Repositories;
using Agence.Api.Domain.Entities;

namespace Agence.Api.Infrastructure.Repositories {
    public class ListingRepository : IListingRepository {

        private static IEnumerable<Listing> listings = new List<Listing>() {
            new Listing() { Id = 1, Title = "First", City = "Mons", Price = 10, Image = "testing"},
            new Listing() { Id = 2, Title = "Second", City = "Brussels", Price = 11, Image = "testing"},
            new Listing() { Id = 3, Title = "Third", City = "Mons", Price = 10, Image = "testing"},
            new Listing() { Id = 4, Title = "Fourth", City = "Mons", Price = 10, Image = "testing"},
        };

        public async Task<IEnumerable<Listing>> SearchListingsAsync(string term) {
            return await Task.Run(() => listings.Where(l => l.Title.Contains(term, StringComparison.InvariantCultureIgnoreCase)));
        }

        public async Task<IEnumerable<Listing>> GetListingsAsync() {
            return await Task.Run(() => listings);
        }
    }
}