using Azure;
using Azure.Communication.Email;
using WebApi.Models;

namespace WebApi.Services;

public class EmailService(IConfiguration configuration, EmailClient client)
{
    private readonly IConfiguration _configuration = configuration;
    private readonly EmailClient _client = client;

    public async Task<bool> SendEmailAsync(EmailSendRequest request)
    {
        var recipients = new List<EmailAddress>();

        foreach (var recipient in request.Recipients)
            recipients.Add(new EmailAddress(recipient));

        var emailMessage = new EmailMessage(
        senderAddress: _configuration["ACS:SenderAddress"],
        content: new EmailContent(request.Subject)
        {
            PlainText = request.PlainText,
            Html = request.Html
        },
        recipients: new EmailRecipients(recipients));

        EmailSendOperation emailSendOperation = await _client.SendAsync(WaitUntil.Completed, emailMessage);
        return emailSendOperation.HasCompleted;
    }
}
