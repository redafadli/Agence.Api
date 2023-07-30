using System;
using Agence.Api.Application.Repositories;
using Agence.Api.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Services
{
	public class EmailService : IEmailService
	{
		private readonly IEmailRepository _emailRepository;

		public EmailService(IEmailRepository emailRepository)
		{
			_emailRepository = emailRepository;
		}

        public Task SendEmailAsync(string toEmail, string subject, string message)
        {
			return _emailRepository.SendEmailAsync(toEmail, subject, message);
        }
    }
}
