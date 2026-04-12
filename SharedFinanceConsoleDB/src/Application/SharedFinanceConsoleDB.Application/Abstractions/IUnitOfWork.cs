namespace SharedFinanceConsoleDB.Application.Abstractions
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}
