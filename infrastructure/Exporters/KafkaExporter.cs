using System;
using System.Collections.Generic;
using System.Net;
using Confluent.Kafka;

namespace infrastructure.Exporters
{
    public class KafkaExporter : IExporter
    {
        private readonly IProducer<Null, string> _producer;
        private Dictionary<object, object> _type2topic;

        public KafkaExporter(Dictionary<string, object> settings)
        {
            var ip = settings["ip"];
            var port = settings["port"];
            var clientId = settings["clientId"];
            _type2topic = (Dictionary<object, object>)settings["type_topic_map"];

            var config = new ProducerConfig
            {
                BootstrapServers = $"{ip}:{port}",
                ClientId = clientId.ToString(),
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public KafkaExporter()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = Dns.GetHostName(),
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();

        }

        public async void Export(Message message)
        {
            var topic = GetTopicByType(message.Type);
            var value = GetValueByPayload(message.Payload);

            await _producer.ProduceAsync(topic, new Message<Null, string> { Value = value });
        }

        private string GetValueByPayload(object payload)
        {
            // TODO: handle this by settings
            return payload.ToString();
        }

        private string GetTopicByType(string type)
        {
            string topic = _type2topic[type].ToString();
            return topic;
        }

        public void Export(object export)
        {
            Export((Message)export);
        }
    }
}

