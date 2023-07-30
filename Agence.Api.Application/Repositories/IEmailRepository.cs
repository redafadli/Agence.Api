using System;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Application.Repositories
{
	public interface IEmailRepository
	{
        Task SendEmailAsync(string toEmail, string subject, string message);

    }
}

