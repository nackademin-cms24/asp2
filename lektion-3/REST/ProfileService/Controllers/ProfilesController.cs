using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Data;
using ProfileService.Models;

namespace ProfileService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfilesController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpPost("create")]
    public async Task<IActionResult> Create(ProfileRegistrationFormData formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var profileEntity = new UserProfileEntity
        {
            UserId = formData.UserId,
            FirstName = formData.FirstName,
            LastName = formData.LastName,
            PhoneNumber = formData.PhoneNumber,
        };

        _context.Add(profileEntity);
        await _context.SaveChangesAsync();

        return Ok();
    }

}
