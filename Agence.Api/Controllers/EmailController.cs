using Agence.Api.Application.Services.Interfaces;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmailController : Controller
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] Email model)
    {
        // Validate the input (e.g., model.ToEmail, model.Subject, model.Message)

        // Send the email using the EmailService
        await _emailService.SendEmailAsync(model.ToEmail, model.Subject, model.Message);

        return Ok("Email sent successfully");
    }
}
