using Azure.Communication.Email;
using Azure.Messaging.ServiceBus;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton(x => new EmailClient(builder.Configuration["ACS:ConnectionString"]));
builder.Services.AddSingleton(x => new ServiceBusClient(builder.Configuration["ASB:ConnectionString"]));
builder.Services.AddSingleton<EmailService>();
builder.Services.AddSingleton<QueueService>();

var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var queueService = app.Services.GetRequiredService<QueueService>();
await queueService.StartAsync();

app.Run();
