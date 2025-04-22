using System.ComponentModel.DataAnnotations;

namespace AccountProvider.Data.Entities;

public class AccountEntity
{
    [Key]
    public string UserId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
