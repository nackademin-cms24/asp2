using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Services;

public class VerificationService
{
    private readonly IConfiguration _configuration;
    private readonly ServiceBusClient _client;
    private readonly ServiceBusSender _sender;
    private static readonly Random _random = new();

    public VerificationService(IConfiguration configuration, ServiceBusClient client)
    {
        _configuration = configuration;
        _client = client;

        _sender = _client.CreateSender(_configuration["ASB:QueueName"]);
    }

    public async Task<bool> SendVerificationCodeAsync(SendVerificationRequest request)
    {
        try
        {
            var emailMessage = GenerateVerficationEmailMessage(request);
            var message = new ServiceBusMessage(emailMessage);

            await _sender.SendMessageAsync(message);
            return true;
        }
        catch 
        {
            return false;
        }
    }

    public string GenerateVerficationEmailMessage(SendVerificationRequest request)
    {
        var verificationCode = _random.Next(100000, 999999).ToString();
        var subject = $"Your code is {verificationCode}";
        var plainText = $@"
            Verify Your Email Address

            Hello,

            To complete your verification, please enter the following code:

            {verificationCode}

            Alternatively, you can open the verification page using the following link:
            https://domain.com/verify?email={request.Email}&token=

            If you did not initiate this request, you can safely disregard this email.
            We take your privacy seriously. No further action is required if you did not initiate this request.

            Privacy Policy:
            https://domain.com/privacy-policy

            © domain.com. All rights reserved.
            ";

        var html = $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
              <meta charset='UTF-8'>
              <title>Your verification code</title>
            </head>
            <body style='margin:0; padding:32px; font-family: Inter, sans-serif; background-color:#F7F7F7; color:#1E1E20;'>
              <div style='max-width:600px; margin:32px auto; background:#FFFFFF; border-radius:16px; padding:32px;'>

                <h1 style='font-size:32px; font-weight:600; color:#37437D; margin-bottom:16px; text-align:center;'>
                  Verify Your Email Address
                </h1>

                <p style='font-size:16px; color:#1E1E20; margin-bottom:16px;'>Hello,</p>

                <p style='font-size:16px; color:#1E1E20; margin-bottom:24px;'>
                  To complete your verification, please enter the code below or click the button to open a new page.
                </p>

                <div style='display:flex; justify-content:center; align-items:center; padding:16px; background-color:#FCD3FE; color:#1C2346; font-size:32px; letter-spacing:0.5rem; border-radius:12px; font-weight:600; margin-bottom:24px;'>
                  {verificationCode}
                </div>

                <div style='text-align:center; margin-bottom:32px;'>
                  <a href='https://domain.com/verify?email={request.Email}&token=' style='background-color:#F26CF9; color:#FFFFFF; padding:12px 24px; border-radius:25px; font-size:16px; text-decoration:none; display:inline-block;'>
                    Open Verification Page
                  </a>
                </div>

                <p style='font-size:12px; color:#777779; text-align:center; margin-top:24px;'>
                  If you did not initiate this request, you can safely disregard this email.
                  <br><br>
                  We take your privacy seriously. No further action is required if you did not initiate this request.
                  For more information about how we process personal data, please see our 
                  <a href='https://domain.com/privacy-policy' style='color:#F26CF9; text-decoration:none;'>Privacy Policy</a>.
                </p>

                <div style='font-size:12px; color:#777779; text-align:center; margin-top:24px;'>
                  © domain.com. All rights reserved.
                </div>

              </div>
            </body>
            </html>
        ";

        var message = new EmailSendRequest
        {
            Recipients = [request.Email],
            Subject = subject,
            PlainText = plainText,
            Html = html,
        };

        var json = JsonConvert.SerializeObject(message);
        return json;
    }
}
