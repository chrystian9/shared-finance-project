using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SharedFinanceConsoleDB.Database;

namespace SharedFinanceConsoleDB.Integration.Tests.Fixtures;

public class DatabaseFixture : IDisposable
{
    private readonly SqliteConnection _connection;
    public SharedFinanceDBContext DbContext { get; private set; }
    public UnitOfWork UnitOfWork { get; private set; }

    public DatabaseFixture()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<SharedFinanceDBContext>()
            .UseSqlite(_connection)
            .Options;

        DbContext = new SharedFinanceDBContext(options);

        DbContext.Database.EnsureCreated();

        UnitOfWork = new UnitOfWork(DbContext);
    }

    public void Dispose()
    {
        _connection.Close();
        DbContext.Dispose();
    }
}
