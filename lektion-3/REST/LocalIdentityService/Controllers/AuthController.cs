using LocalIdentityService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LocalIdentityService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(UserManager<IdentityUser> userManager) : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager = userManager;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationFormData formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new IdentityUser
        {
            UserName = formData.Email,
            Email = formData.Email,
        };

        var result = await _userManager.CreateAsync(user, formData.Password);
        if (result.Succeeded)
        {
            var profile = new ProfileRegistrationFormData
            {
                UserId = user.Id,
                FirstName = formData.FirstName,
                LastName = formData.LastName,
                PhoneNumber = formData.PhoneNumber,
            };

            using var http = new HttpClient();
            var response = await http.PostAsJsonAsync("https://localhost:7030/api/profiles/create", profile);

            return Ok();
        }

        return BadRequest();
    }
}
