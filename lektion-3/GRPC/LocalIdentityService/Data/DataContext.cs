using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LocalIdentityService.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext(options)
{
}
