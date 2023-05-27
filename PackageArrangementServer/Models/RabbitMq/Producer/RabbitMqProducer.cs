using RabbitMQ.Client;
using System.Text;

namespace PackageArrangementServer.Models
{
    public class RabbitMqProducer // : IDisposable
    {
        protected const string LocalHost = "localhost";
        protected readonly string ExchangeName = $"{LocalHost}.Exchange";
        private IConnection _connection = null;

        public RabbitMqProducer()
        {
            //_connection = ConnectToRabbitMq();
        }

        private IConnection ConnectToRabbitMq()
        {
            ConnectionFactory factory = new ConnectionFactory();

            factory.UserName = "user";
            factory.Password = "pass";
            factory.Port = 5672;
            factory.HostName = LocalHost;
            factory.VirtualHost = "/";

            return factory.CreateConnection();
        }
        
        public IConnection GetConnection() { return _connection; }

        public bool Send(string message, string friendqueue)
        {
            return Send(_connection, message, friendqueue);
        }

        public bool Send(IConnection connection, string message, string friendqueue)
        {
            // if (connection == null || connection.IsOpen == false) connection = GetConnection(); // or maybe just return false
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

        /*public void Dispose(IModel channel, IConnection connection)
        {
            try
            {
                channel?.Close();
                channel?.Dispose();
                channel = null;

                connection?.Close();
                connection?.Dispose();
                connection = null;
            }
            catch (Exception ex)
            {
                //_logger.LogCritical(ex, "Cannot dispose RabbitMq channel or connection.");
            }
        }*/
    }
}
