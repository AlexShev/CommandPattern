using Laba3.commands;

namespace Laba3;

class Editor
{
    private readonly Text _text;
    private readonly string _path;

    private readonly Stack<ICommand> _history;
    private readonly Stack<ICommand> _undo;

    public bool IsSaved { get; private set; } = true;

    public Editor(Text text, string path)
    {
        _text = text;
        _path = path;
        _history = new();
        _undo = new();
    }
    public Editor(string path) : this(new Text(), path) { }


    public string Text => _text.ToString();
    public int Size => _text.Count;
    public bool IsComplete { private set; get; } = false;
    public string? LineAt(int index) => _text.Count > index && -1 < index ? _text.Lines[index] : null;

    private void Execute(ICommand comand)
    {
        comand.Execute();
        IsComplete = comand.IsComplete;
        if (IsComplete)
        {
            IsSaved = false;
            _history.Push(comand);
        }
    }
    
    private void Undo(ICommand comand)
    {
        IsSaved = false;
        comand.Undo();
        _undo.Push(comand);
    }


    public void Undo() => Undo(_history.Pop());
    public bool CanUndo() => _history.Count > 0;
    public void Redo() => Execute(_undo.Pop());
    public bool CanRedo() => _undo.Count > 0;

    private void SaveCancelledCommand()
    {
        if(_undo.Count > 0)
        {
            var cancelledCommands = new Stack<ICommand>();

            while (_undo.Count > 0)
            {
                _history.Push(_undo.Peek());
                cancelledCommands.Push(new CancelledCommand(_undo.Pop()));
            }

            while (cancelledCommands.Count > 0)
            {
                _history.Push(cancelledCommands.Pop());
            }
        }
    }

    public void Insert(int index, string str)
    {
        SaveCancelledCommand();

        Execute(new Insert(_text, index, str));
    }
    public void RemoveAt(int index)
    {
        SaveCancelledCommand();

        Execute(new RemoveAt(_text, index));
    }
    public void ReplaceAt(int index, string str)
    {
        SaveCancelledCommand();

        Execute(new ReplaceAt(_text, index, str));
    }
    public void Delete()
    {
        SaveCancelledCommand();

        Execute(new Delete(_text, _path));
    }
    public void Save()
    {
        new Save(_text, _path).Execute();
        
        IsSaved = true;

        _history.Clear();
        _undo.Clear();
    }
}
