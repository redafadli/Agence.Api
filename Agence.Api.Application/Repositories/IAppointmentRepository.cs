using System;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Repositories
{
	public interface IAppointmentRepository
	{
        Task<IEnumerable<Appointment>> getAppointmentByEmailAsync(string user_email);
        Task<IActionResult> postAppointmentAsync(Appointment appointment);
        Task<IActionResult> deleteAppointmentAsync(int appointment_id);
        Task<Appointment> getAppointmentByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetAppointmentsAsync();
    }
}

