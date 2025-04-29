using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Services;

public class QueueService : IAsyncDisposable
{
    private readonly IConfiguration _configuration;
    private readonly ServiceBusClient _client;
    private readonly EmailService _emailService;
    private readonly ServiceBusProcessor _processor;


    public QueueService(IConfiguration configuration, ServiceBusClient client, EmailService emailService)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _emailService = emailService;

        var queueName = _configuration["ASB:QueueName"] ?? throw new InvalidOperationException("QueueName is not configured.");
        _processor = _client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

        RegisterMessageHandler();
        RegisterErrorHandler();

    }

    private void RegisterMessageHandler()
    {
        _processor.ProcessMessageAsync += async args =>
        {
            try
            {
                var body = args.Message.Body.ToString();

                var emailSendRequest = JsonConvert.DeserializeObject<EmailSendRequest>(body) ?? throw new Exception("Unable to deserialize request");

                var result = await _emailService.SendEmailAsync(emailSendRequest);
                if (result)
                    await args.CompleteMessageAsync(args.Message);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Message handling failed: {ex.Message}");
                await args.AbandonMessageAsync(args.Message);
            }
        };
    }

    private void RegisterErrorHandler()
    {
        _processor.ProcessErrorAsync += args =>
        {
            Console.WriteLine($"Error processing message: {args.Exception.Message}");
            return Task.CompletedTask;
        };
    }

    public async Task StartAsync()
    {
        await _processor.StartProcessingAsync();
        Console.WriteLine("Queue processing started.");
    }

    public async Task StopAsync()
    {
        await _processor.StopProcessingAsync();
        Console.WriteLine("Queue processing stopped.");
    }

    public async ValueTask DisposeAsync()
    {
        if (_processor is not null)
        {
            await _processor.DisposeAsync();
        }

        if (_client is not null)
        {
            await _client.DisposeAsync();
        }
    }
}
