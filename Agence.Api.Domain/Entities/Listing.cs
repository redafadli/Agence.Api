using System;
namespace Agence.Api.Domain.Entities {
    public class Listing {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}