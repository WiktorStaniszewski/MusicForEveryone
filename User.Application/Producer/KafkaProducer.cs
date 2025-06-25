using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace User.Application.Producer;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<Null, string> _producer;

    public KafkaProducer(IConfiguration config)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = config["Kafka:BootstrapServers"]
        };
        _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    }


    public async Task SendAsync(string topic, string message)
    {
        await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
    }
}