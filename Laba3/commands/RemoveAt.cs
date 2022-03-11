namespace Laba3.commands;

class RemoveAt : ICommand
{
    private readonly Text _text;
    private readonly int _index;
    private string _str = null;

    public RemoveAt(Text text, int index)
    {
        _text = text;
        _index = index;
    }

    public bool IsComplete { get; private set; }

    public void Execute()
    {
        IsComplete = _index < _text.Count && _index >= 0 && !IsComplete;

        if (IsComplete)
        {
            _str = _text.Lines[_index];
            _text.Lines.RemoveAt(_index);
        }
    }

    public void Undo()
    {
        if (IsComplete)
        {
            _text.Lines.Insert(_index, _str);
            IsComplete = false;
        }
    }
}
