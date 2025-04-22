using System.ComponentModel.DataAnnotations;

namespace AccountProvider.Models;

public class CreateRequest
{
    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;
}
