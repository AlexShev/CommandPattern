namespace Laba3.commands;

interface ICommand
{
    void Execute();
    void Undo();
    bool IsComplete { get; }
}
