using System;
using Agence.Api.Application.Repositories;
using Agence.Api.Application.Services.Interfaces;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Services
{
    public class AppointmentService : IAppointmentService
	{
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
		{
            _appointmentRepository = appointmentRepository;
		}

        public Task<IActionResult> deleteAppointmentAsync(int appointment_id)
        {
            return _appointmentRepository.deleteAppointmentAsync(appointment_id);
        }

        public Task<IEnumerable<Appointment>> getAppointmentByEmailAsync(string user_email)
        {
            return _appointmentRepository.getAppointmentByEmailAsync(user_email);
        }

        public Task<Appointment> getAppointmentByIdAsync(int id)
        {
            return _appointmentRepository.getAppointmentByIdAsync(id);
        }

        public Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            return _appointmentRepository.GetAppointmentsAsync();
        }

        public Task<IActionResult> postAppointmentAsync(Appointment appointment)
        {
            return _appointmentRepository.postAppointmentAsync(appointment);
        }
    }
}

