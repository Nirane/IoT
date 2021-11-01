using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using api.Models;

namespace api.Services
{
    public class AskerService
    {
        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;

        private readonly SensorService _sensorService;

        public AskerService(SensorService sensorService)
        {
            _sensorService = sensorService;

            while (true)
            {
                try
                {
                    System.Threading.Thread.Sleep(5000);

                    factory = new ConnectionFactory() { HostName = "rabbit" };
                    connection = factory.CreateConnection();

                    channel = connection.CreateModel();

                    channel.QueueDeclare(queue: "dotNetProject",
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        SensorDataDto sensorData = JsonSerializer.Deserialize<SensorDataDto>(body);

                        SensorData sData = new SensorData();
                        sData.SensorId = sensorData.SensorId;
                        sData.Type = sensorData.SensorType;
                        sData.Value = sensorData.Value;
                        sData.Date = sensorData.Date;

                        _sensorService.Create(sData);
                    };
                    channel.BasicConsume(queue: "dotNetProject",
                                            autoAck: true,
                                            consumer: consumer);
                    break;
                }
                catch
                {

                }
            }
        }
    }
}
