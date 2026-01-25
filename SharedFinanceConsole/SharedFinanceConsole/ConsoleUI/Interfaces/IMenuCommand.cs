namespace SharedFinanceConsole.ConsoleUI.Interfaces
{
    public interface IMenuCommand
    {
        public string Label { get; }
        public void Execute();
    }
}
