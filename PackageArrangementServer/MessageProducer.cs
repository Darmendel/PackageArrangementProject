using System;
using System.Text;
using RabbitMQ.Client;

namespace PackageArrangementServer
{
    public class MessageProducer
    {
        private const string _userName = "guest";
        private const string _password = "guest";
        private const string _hostName = "localhost";

        public void Send()
        {
            var connectionFactory = new ConnectionFactory()
            {
                UserName = _userName,
                Password = _password,
                HostName = _hostName
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();
            var properties = model.CreateBasicProperties();
            properties.Persistent = false;
            byte[] messagebuffer = Encoding.Default.GetBytes("Direct Message");
            model.BasicPublish("request.exchange", "directexchange_key", properties, messagebuffer);
            Console.WriteLine("Message Sent");
        }
    }
}
