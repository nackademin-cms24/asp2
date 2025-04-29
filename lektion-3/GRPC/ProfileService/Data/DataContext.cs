using Microsoft.EntityFrameworkCore;

namespace ProfileService.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<ProfileEntity> Profiles { get; set; }
}
