using System;
namespace Agence.Api.Domain.Entities
{
	public class Favorite
	{
		public int Id { get; set; }
        public string User_email { get; set; }
        public int Listing_id { get; set; }
    }
}

