﻿namespace Bioworld.WebApi.Requests
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    public class RequestDispatcher : IRequestDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public RequestDispatcher(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task<TResult> DispatchAsync<TRequest, TResult>(TRequest request) where TRequest : class, IRequest
        {
            using var scope = _serviceFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<TRequest, TResult>>();
            return await handler.HandleAsync(request);
        }
    }
}