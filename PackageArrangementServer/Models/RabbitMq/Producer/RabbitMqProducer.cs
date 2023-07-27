using RabbitMQ.Client;
using System.Text;

namespace PackageArrangementServer.Models
{
    public class RabbitMqProducer
    {
        const int PORT = 5672;
        const string USER = "user";
        const string PASSWORD = "pass";
        const string LOCALHOST = "localhost";
        const string VH = "/";

        protected readonly string ExchangeName = $"{LOCALHOST}.Exchange";
        private IConnection _connection = null;

        public RabbitMqProducer()
        {
            _connection = ConnectToRabbitMq();
        }

        private IConnection ConnectToRabbitMq()
        {
            ConnectionFactory factory = new ConnectionFactory();

            factory.UserName = USER;
            factory.Password = PASSWORD;
            factory.Port = PORT;
            factory.HostName = LOCALHOST;
            factory.VirtualHost = VH;

            return factory.CreateConnection();
        }
        
        public IConnection GetConnection() { return _connection; }

        public bool Send(string message, string friendqueue)
        {
            return Send(_connection, message, friendqueue);
        }

        public bool Send(IConnection connection, string message, string friendqueue)
        {
            if (connection == null || message == null || friendqueue == null) return false;

            try
            {
                IModel channel = connection.CreateModel();

                channel.ExchangeDeclare(
                    exchange: ExchangeName,
                    type: ExchangeType.Direct,
                    durable: true,
                    autoDelete: false);

                channel.QueueDeclare(
                    queue: friendqueue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false);

                channel.QueueBind(
                    queue: friendqueue,
                    exchange: ExchangeName,
                    routingKey: friendqueue,
                    arguments: null);

                var msg = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: ExchangeName,
                    routingKey: friendqueue,
                    basicProperties: null,
                    body: msg);

            }
            catch (Exception)
            {
            }

            Console.WriteLine($" [x] Sent {message}");
            return true;
        }
    }
}
