namespace PackageArrangementServer.Services
{
    public interface IRabbitMqProducer<in T> // maybe change T into some kind of list/JSON
    {
        void Publish(T @event);
    }
}
