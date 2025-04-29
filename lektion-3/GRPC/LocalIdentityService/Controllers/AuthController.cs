using LocalIdentityService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LocalIdentityService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(UserManager<IdentityUser> userManager, ProfileContract.ProfileContractClient profileClient) : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly ProfileContract.ProfileContractClient _profileClient = profileClient;


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
            var request = new ProfileRegistrationRequest
            {
                UserId = user.Id,
                FirstName = formData.FirstName,
                LastName = formData.LastName,
                PhoneNumber = formData.PhoneNumber,
            };

            var response = await _profileClient.RegisterProfileAsync(request);
            return response.Succeeded ? Ok() : BadRequest(response.Message);
        }

        return BadRequest();
    }
}
