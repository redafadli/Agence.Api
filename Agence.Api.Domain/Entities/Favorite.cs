using System;
namespace Agence.Api.Domain.Entities
{
	public class Favorite
	{
		public int Id { get; set; }
        public string UserEmail { get; set; }
        public int ListingId { get; set; }
    }
}

