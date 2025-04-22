using AccountProvider.Data.Contexts;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);
builder.ConfigureFunctionsWebApplication();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnection")));



builder.Build().Run();
