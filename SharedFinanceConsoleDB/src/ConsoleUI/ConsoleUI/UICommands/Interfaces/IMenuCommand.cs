namespace ConsoleUI.UICommands.Interfaces
{
    public interface IMenuCommand
    {
        public string Label { get; }
        public void Execute();
    }
}
