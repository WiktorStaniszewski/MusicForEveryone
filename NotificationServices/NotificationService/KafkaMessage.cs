using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NotificationService;

public class KafkaMessage
{
    public long Offset { get; set; }
    public int Partition { get; set; }
    public string Topic { get; set; }
    public DateTime Timestamp { get; set; }
    public string Value { get; set; }
    public string Key { get; set; }
    public List<object> Headers { get; set; }
}