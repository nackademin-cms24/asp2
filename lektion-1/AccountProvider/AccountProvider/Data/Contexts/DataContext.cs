using AccountProvider.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountProvider.Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<AccountEntity> Accounts { get; set; }
}
