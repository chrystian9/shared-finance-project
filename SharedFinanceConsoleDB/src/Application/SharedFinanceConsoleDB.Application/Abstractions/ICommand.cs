namespace SharedFinanceConsoleDB.Application.Abstractions
{
    public interface ICommand<TResult> : IRequest<TResult> { }
}
