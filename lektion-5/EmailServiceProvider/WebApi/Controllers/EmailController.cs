using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController(EmailService emailService) : ControllerBase
{
    private readonly EmailService _emailService = emailService;

    [HttpPost("send")]
    public async Task<IActionResult> Send(EmailSendRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        if (request.Recipients == null || request.Recipients.Count == 0) 
            return BadRequest(new { error = "recipients are required."});

        var result = await _emailService.SendEmailAsync(request);
        return result ? Ok() : StatusCode(500, new { error = "Unable to send email." });
    }
}
