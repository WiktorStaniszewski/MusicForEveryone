using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Azure.WebJobs;

namespace NotificationService;

public class LoginNotificationEmail
{
    private readonly ILogger<LoginNotificationEmail> _logger;

    public LoginNotificationEmail(ILogger<LoginNotificationEmail> logger)
    {
        _logger = logger;
    }

    [FunctionName("KafkaTriggerFunction")]
    public async Task Run(
        [KafkaTrigger(
            "broker1:9092",
            "topic-name",
            ConsumerGroup = "group-id")] KafkaEventData<string> message,
        ILogger log)
    {
        log.LogInformation($"Received: {message.Value}");
        await SendEmailAsync("Just logged in", message.Value);
    }

    static async Task SendEmailAsync(string message, string toEmail)
    {
        try
        {
            string smtpHost = Environment.GetEnvironmentVariable("smtpHost");
            int smtpPort = Int32.Parse(Environment.GetEnvironmentVariable("smtpPort"));
            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.EnableSsl = true;
                string smtpUsername = Environment.GetEnvironmentVariable("smtpUsername");
                string smtpPassword = Environment.GetEnvironmentVariable("smtpPassword");
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("test@wiktor.pl"),
                    Subject = "Kafka Message",
                    Body = message,
                    IsBodyHtml = false
                };
                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
                Console.WriteLine($"E-mail sent to {toEmail} with a message: {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while sending an email: {ex.Message}");
        }
    }
}