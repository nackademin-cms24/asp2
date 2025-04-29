using Azure.Messaging.ServiceBus;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton(x => new ServiceBusClient(builder.Configuration["ASB:ConnectionString"]));
builder.Services.AddTransient<VerificationService>();

var app = builder.Build();
app.MapOpenApi();
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
