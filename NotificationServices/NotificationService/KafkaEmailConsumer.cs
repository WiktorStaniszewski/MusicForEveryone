using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

public class KafkaEmailConsumer : BackgroundService
{
    private readonly IConfiguration _config;
    private readonly ILogger<KafkaEmailConsumer> _logger;

    public KafkaEmailConsumer(IConfiguration config, ILogger<KafkaEmailConsumer> logger)
    {
        _config = config;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _config["Kafka:BootstrapServers"],
            GroupId = "email-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        consumer.Subscribe("user-registration-topic");

        _logger.LogInformation("Kafka email consumer started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(stoppingToken);
                var recipientEmail = result.Message.Value;

                _logger.LogInformation($"Received email trigger for: {recipientEmail}");

                await SendEmailAsync("Confirm your registration", recipientEmail);
            }
            catch (ConsumeException e)
            {
                _logger.LogError($"Consume error: {e.Error.Reason}");
            }
        }
    }

    private async Task SendEmailAsync(string subject, string toEmail)
    {
        try
        {
            var smtpHost = _config["Smtp:Host"];
            var smtpPort = int.Parse(_config["Smtp:Port"]);
            var smtpUser = _config["Smtp:Username"];
            var smtpPass = _config["Smtp:Password"];

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(smtpUser, smtpPass)
            };

            var mail = new MailMessage
            {
                From = new MailAddress("no-reply@example.com"),
                Subject = subject,
                Body = "Thank you for registering.",
                IsBodyHtml = false
            };
            mail.To.Add(toEmail);

            await client.SendMailAsync(mail);

            _logger.LogInformation($"Email sent to {toEmail}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Email sending failed: {ex.Message}");
        }
    }
}
