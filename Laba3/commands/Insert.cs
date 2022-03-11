namespace Laba3.commands;

class Insert : ICommand
{
    private readonly Text _text;
    private readonly int _index;
    private readonly string _str;

    public Insert(Text text, int index, string str)
    {
        _text = text;
        _index = index;
        _str = str;
    }

    public bool IsComplete { get; private set; }

    public void Execute()
    {
        IsComplete = _index <= _text.Count && _index >= 0 && !IsComplete;

        if (IsComplete)
        {
            _text.Lines.Insert(_index, _str);
        }
    }

    public void Undo()
    {
        if (IsComplete)
        {
            _text.Lines.RemoveAt(_index);
            IsComplete = false;
        }
    }
}
