using RabbitMQ.Client;

namespace PackageArrangementServer.Services
{
    public abstract class RabbitMqClientBase : IDisposable
    {
        protected const string VirtualHost = "CUSTOM_HOST";
        protected readonly string LoggerExchange = $"{VirtualHost}.LoggerExchange";
        protected readonly string LoggerQueue = $"{VirtualHost}.log.message";
        protected const string LoggerQueueAndExchangeRoutingKey = "log.message";
        protected IModel Channel { get; private set; }
        private IConnection _connection;
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMqClientBase> _logger;

        protected RabbitMqClientBase(ConnectionFactory connectionFactory, ILogger<RabbitMqClientBase> logger)
        {
            this._connectionFactory = connectionFactory;
            this._logger = logger;
            ConnectToRabbitMq();
        }

        /// <summary>
        /// Connecting to RabbitMq by checking if the connection is already established or not, and the same for the channel.
        /// </summary>
        private void ConnectToRabbitMq()
        {
            if (_connection == null || _connection.IsOpen == false) _connection = _connectionFactory.CreateConnection();

            if (Channel == null || Channel.IsOpen == false)
            {
                Channel = _connection.CreateModel();

                Channel.ExchangeDeclare(
                    exchange: LoggerExchange, // The exchange name.
                    type: "direct",
                    durable: true,
                    autoDelete: false);

                Channel.QueueDeclare(
                    queue: LoggerQueue, // The queue name.
                    durable: false,
                    exclusive: false,
                    autoDelete: false);

                Channel.QueueBind(
                    queue: LoggerQueue, // The queue name.
                    exchange: LoggerExchange, // The exchange name.
                    routingKey: LoggerQueueAndExchangeRoutingKey);
            }
        }

        public void Dispose()
        {
            try
            {
                Channel?.Close();
                Channel?.Dispose();
                Channel = null;

                _connection?.Close();
                _connection?.Dispose();
                _connection = null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Cannot dispose RabbitMq channel or connection.");
            }
        }
    }
}
