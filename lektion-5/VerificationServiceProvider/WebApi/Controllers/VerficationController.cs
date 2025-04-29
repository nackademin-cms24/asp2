using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[Route("api/verification")]
[ApiController]
public class VerficationController(VerificationService verificationService) : ControllerBase
{
    private readonly VerificationService _verificationService = verificationService;

    [HttpPost("send")]
    public async Task<IActionResult> Send(SendVerificationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { Error = "Recipient email address is required." });

        var result = await _verificationService.SendVerificationCodeAsync(request);
        return result ? Ok() : StatusCode(500, "Unable to send verification code.");
    }

    //[HttpPost("verify")]
    //public IActionResult Verify(VerifyVerificationRequest request)
    //{
    //    if (!ModelState.IsValid)
    //        return BadRequest(new { Error = "Invalid or expired verification code." });
    //}
}
