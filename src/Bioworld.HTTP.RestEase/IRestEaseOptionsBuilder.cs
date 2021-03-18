namespace Bioworld.HTTP.RestEase
{
    using System;

    public interface IRestEaseOptionsBuilder
    {
        IRestEaseOptionsBuilder WithLoadBalancer(string loadBalancer);
        IRestEaseOptionsBuilder WithService(Func<IRestEaseServiceBuilder, IRestEaseServiceBuilder> buildService);
        RestEaseOptions Build();
    }
}