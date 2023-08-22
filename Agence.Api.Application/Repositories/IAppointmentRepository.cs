using System;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Repositories
{
	public interface IAppointmentRepository
	{
        Task<IEnumerable<Appointment>> GetAppointmentByEmailAsync(string user_email);
        Task<IActionResult> PostAppointmentAsync(Appointment appointment);
        Task<IActionResult> DeleteAppointmentAsync(int appointment_id);
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetAppointmentsAsync();
    }
}

