﻿public interface IKafkaProducer
{
    Task SendAsync(string topic, string message);
}
