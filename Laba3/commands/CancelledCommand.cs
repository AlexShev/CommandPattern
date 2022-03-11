namespace Laba3.commands;

class CancelledCommand : ICommand
{
    private readonly ICommand _comand;

    public CancelledCommand(ICommand comand)
    {
        _comand = comand;
    }

    public bool IsComplete => !_comand.IsComplete;

    public void Execute()
    {
        if (!IsComplete)
        {
            _comand.Undo();
        }
    }

    public void Undo()
    {
        if (IsComplete)
        {
            _comand.Execute();
        }
    }
}
