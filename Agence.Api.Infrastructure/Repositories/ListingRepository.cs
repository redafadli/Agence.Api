using System;
using Agence.Api.Application.Repositories;
using Agence.Api.Domain.Entities;

namespace Agence.Api.Infrastructure.Repositories {
    public class ListingRepository : IListingRepository {

        private static readonly IEnumerable<Listing> Listings = new List<Listing>() {
            new Listing() { Id = 1, Title = "First", City = "Mons", Price = 10, Image = "https://mdbootstrap.com/img/Photos/Slides/img%20(15).webp"},
            new Listing() { Id = 2, Title = "Second", City = "Brussels", Price = 11, Image = "https://mdbootstrap.com/img/Photos/Slides/img%20(23).webp"},
            new Listing() { Id = 3, Title = "Third", City = "Mons", Price = 10, Image = "https://mdbootstrap.com/img/Photos/Slides/img%20(12).webp"},
            new Listing() { Id = 4, Title = "Fourth", City = "Mons", Price = 10, Image = "https://mdbootstrap.com/img/Photos/Slides/img%20(11).webp"},
        };

        public async Task<IEnumerable<Listing>> SearchListingsAsync(string term) {
            return await Task.Run(() => Listings.Where(l => l.Title.Contains(term, StringComparison.InvariantCultureIgnoreCase)));
        }
<<<<<<< Updated upstream
=======

        public async Task<IEnumerable<Listing[]>> GetListingsAsync() {

        }
    }
}
>>>>>>> Stashed changes

        public async Task<IEnumerable<Listing>> GetListingsAsync() {
            return await Task.Run(() => Listings);
        }
        public async Task<Listing> GetListingAsync(int id) {
            return await Task.Run(() => Listings.FirstOrDefault(l => l.Id == id));
        }
    }
}