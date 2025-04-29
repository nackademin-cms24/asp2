namespace WebApi.Models;

public class VerifyVerificationRequest
{
    public string Email { get; set; } = null!;
    public string Code { get; set; } = null!;
}