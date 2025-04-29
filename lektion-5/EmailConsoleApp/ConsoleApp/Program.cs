using System;
using System.Collections.Generic;
using Azure;
using Azure.Communication.Email;

string connectionString = "endpoint=https://cms24-cs.europe.communication.azure.com/;accesskey=1DViZrHRP3Q279MBuB04IxnhmoT1Mv2wMbZsDqtiFqleVxE6siyQJQQJ99BDACULyCpHNs5HAAAAAZCSsJKT";
var emailClient = new EmailClient(connectionString);


Console.Write("To: ");
var to = Console.ReadLine();
Console.Write("Subject: ");
var subject = Console.ReadLine();
Console.Write("Message: ");
var body = Console.ReadLine();



var emailMessage = new EmailMessage(
    senderAddress: "DoNotReply@30f3c17d-4178-4f90-a25b-63852b680c7e.azurecomm.net",
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