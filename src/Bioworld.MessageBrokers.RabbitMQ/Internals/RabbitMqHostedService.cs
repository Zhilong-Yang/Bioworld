using RabbitMQ.Client;

namespace Bioworld.MessageBrokers.RabbitMQ.Internals
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;

    internal sealed class RabbitMqHostedService : IHostedService
    {
        private readonly IConnection _connection;

        public RabbitMqHostedService(IConnection connection)
        {
            _connection = connection;
        }

        public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                _connection.Close();
            }
            catch
            {
                // ignored
            }

            return Task.CompletedTask;
        }
    }
}