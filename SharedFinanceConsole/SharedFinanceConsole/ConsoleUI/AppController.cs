namespace SharedFinanceConsole.ConsoleUI
{
    public class AppController
    {
        public bool IsRunning { get; private set; } = true;

        public void Stop() => IsRunning = false;
    }
}
