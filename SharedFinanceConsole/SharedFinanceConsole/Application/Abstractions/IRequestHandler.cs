namespace SharedFinanceConsole.Application.Abstractions
{
    public interface IRequestHandler<TRequest, TResult> 
        where TRequest : IRequest<TResult>
    {
        TResult Handle(TRequest request);
    }
}
