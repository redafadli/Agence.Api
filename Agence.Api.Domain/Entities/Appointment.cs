using System;
namespace Agence.Api.Domain.Entities
{
	public class Appointment
	{
        public int Id { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string? UserEmail { get; set; }
        public int ListingId { get; set; }
	}
}
