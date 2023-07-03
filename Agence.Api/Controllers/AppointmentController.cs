using System;
using Agence.Api.Application.Services;
using Agence.Api.Application.Services.Interfaces;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
		public readonly IAppointmentService _appointmentService;

		public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService?? throw new ArgumentNullException(nameof(appointmentService));
        }

        [HttpGet("/Appointments")]
        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            return await _appointmentService.GetAppointmentsAsync();
        }

        [HttpPost]
        public async Task<IActionResult> PostAppointmentAsync([FromBody] Appointment appointment)
        {
            return await _appointmentService.postAppointmentAsync(appointment);
        }

        [HttpGet("/Appointment/user_email/{user_email}")]
        public async Task<IEnumerable<Appointment>> getAppointmentByEmailAsync([FromRoute]string user_email)
        {
            return await _appointmentService.getAppointmentByEmailAsync(user_email);
        }

        [HttpGet("/Appointment/id/{id}")]
        public async Task<Appointment> getAppointmentByIdAsync(int id)
        {
            return await _appointmentService.getAppointmentByIdAsync(id);
        }

        [HttpDelete("/Appointment/delete/{appointment_id}")]
        public async Task<IActionResult> deleteAppointmentAsync(int appointment_id)
        {
            return await _appointmentService.deleteAppointmentAsync(appointment_id);
        }
    }
}

