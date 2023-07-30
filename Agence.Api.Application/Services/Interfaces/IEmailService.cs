using System;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Services.Interfaces
{
	public interface IEmailService
	{
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}

