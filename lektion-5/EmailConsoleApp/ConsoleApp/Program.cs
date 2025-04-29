using System;
using System.Collections.Generic;
using Azure;
using Azure.Communication.Email;

string connectionString = "";
var emailClient = new EmailClient(connectionString);


Console.Write("To: ");
var to = Console.ReadLine();
Console.Write("Subject: ");
var subject = Console.ReadLine();
Console.Write("Message: ");
var body = Console.ReadLine();



var emailMessage = new EmailMessage(
    senderAddress: "",
    content: new EmailContent(subject)
    {
        PlainText = $"{body}",
        Html = $@"
		<html>
			<body>
				{body}
			</body>
		</html>"
    },
    recipients: new EmailRecipients([new(to)]));


EmailSendOperation emailSendOperation = emailClient.Send(
    WaitUntil.Completed,
    emailMessage);

Console.WriteLine("Email sent.");
Console.ReadKey();