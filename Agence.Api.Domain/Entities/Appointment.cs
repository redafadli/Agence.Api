using System;
namespace Agence.Api.Domain.Entities
{
	public class Appointment
	{
        public int Id { get; set; }
        public DateTime Appointment_date_time { get; set; }
        public string? User_email { get; set; }
        public int Listing_id { get; set; }
	}
}
