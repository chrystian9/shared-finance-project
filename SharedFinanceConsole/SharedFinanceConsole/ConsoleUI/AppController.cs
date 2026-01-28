using SharedFinanceConsole.Application.Abstractions;

namespace SharedFinanceConsole.ConsoleUI
{
    public class AppController
    {
        private readonly Dictionary<Type, object> _handlers = [];

        public bool IsRunning { get; private set; } = true;

        public void Stop() => IsRunning = false;

        public void RegisterHandler<TRequest, TResult>(IRequestHandler<TRequest, TResult> handler)
            where TRequest : IRequest<TResult>
        {
            _handlers[typeof(TRequest)] = handler;
        }

        public TResult Send<TResult>(IRequest<TResult> request)
        {
            if (!_handlers.TryGetValue(request.GetType(), out var handler))
                throw new Exception($"No handler registered for {request.GetType().Name}");

            return ((dynamic)handler).Handle((dynamic)request);
        }
    }
}
