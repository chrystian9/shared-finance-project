namespace SharedFinanceConsole.Application.Abstractions
{
    public interface ICommand<TResult> : IRequest<TResult> { }
}
