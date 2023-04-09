using System;
using Agence.Api.Application.Repositories;
using Agence.Api.Domain.Entities;

namespace Agence.Api.Infrastructure.Repositories {
    public class ListingRepository : IListingRepository {

        private static readonly IEnumerable<Listing> Listings = new List<Listing>() {
            new Listing() { Id = 1, Title = "First", City = "Mons", Price = 100.000, Image = "https://mdbootstrap.com/img/Photos/Slides/img%20(15).webp", Address="Rue adelson castiaux 219",
                Description = "Rue de Beugnies 4D. Très belle maison de standing (228 M²) entièrement rafraichi comp.Au rez: hall d'entrée avec wc séparé (lave-mains), double garage de 32m², grande buanderie de 11,5m², pièce de stockage de +/- 35m². 1er étage: grand séjour de +/-50m² avec feu ouvert, accès terrasse, cuisine entièrement équipée , hall de nuit, 1 suite parentale avec chambre de 25m², salle de bain avec baignoire et wc, une chambre de 24,5m², une deuxième salle de bain avec baignoire et wc, et une troisième avec placards de 21m². Jardin. Chauffage central mazout. Libre d'occupation. PEB: E 384 kWh/m².an.La maison a été intégralement isolée. Loyer: 1.500 € / mois. Pas de charges communes.Un nouveau PEB est en cours car le bien a été récemment isolé par les propriétaires.Infos et visites: Alpimmo au 065/34.84.24 ou Mlle Pollart au 0473/72.71.12"},
            new Listing() { Id = 2, Title = "Second", City = "Brussels", Price = 11.000, Image = "https://mdbootstrap.com/img/Photos/Slides/img%20(23).webp", Description = "Testing"},
            new Listing() { Id = 3, Title = "Third", City = "Mons", Price = 25.400, Image = "https://mdbootstrap.com/img/Photos/Slides/img%20(13).webp", Description = "Testing"},
            new Listing() { Id = 4, Title = "Fourth", City = "Mons", Price = 750.000, Image = "https://mdbootstrap.com/img/Photos/Slides/img%20(11).webp", Description = "Testing"},
            new Listing() { Id = 5, Title = "Fifth", City = "Mons", Price = 10.000, Image = "https://mdbootstrap.com/img/Photos/Slides/img%20(45).webp", Description = "Testing"},
            new Listing() { Id = 6, Title = "Sixth", City = "Mons", Price = 15.000, Image = "https://mdbootstrap.com/img/Photos/Slides/img%20(25).webp", Description = "Testing"},
            new Listing() { Id = 7, Title = "Seventh", City = "Mons", Price = 20.000, Image = "https://mdbootstrap.com/img/Photos/Slides/img%20(7).webp", Description = "Testing"},
        };

        public async Task<IEnumerable<Listing>> SearchListingsAsync(string term) {
            return await Task.Run(() => Listings.Where(l => l.Title.Contains(term, StringComparison.InvariantCultureIgnoreCase)));
        }

        public async Task<IEnumerable<Listing>> GetListingsAsync() {
            return await Task.Run(() => Listings);
        }
        public async Task<Listing> GetListingAsync(int id) {
            return await Task.Run(() => Listings.FirstOrDefault(l => l.Id == id));
        }
    }
}