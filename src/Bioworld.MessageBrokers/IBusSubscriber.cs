namespace Bioworld.MessageBrokers
{
    using System;
    using System.Threading.Tasks;

    public interface IBusSubscriber : IDisposable
    {
        IBusSubscriber Subscribe<T>(Func<IServiceProvider, T, object, Task> handle) where T : class;
    }
}