namespace Laba3.commands;

class ReplaceAt : ICommand
{
    private readonly Text _text;
    private readonly int _index;
    private string _str;

    public ReplaceAt(Text text, int index, string str)
    {
        _text = text;
        _index = index;
        _str = str;
        IsComplete = false;
    }

    public bool IsComplete { get; private set; }

    public void Execute()
    {
        IsComplete = _index < _text.Count && _index >= 0 && !IsComplete;

        if (IsComplete)
        {
            (_text.Lines[_index], _str) = (_str, _text.Lines[_index]);
        }
    }

    public void Undo()
    {
        if (IsComplete)
        {
            (_text.Lines[_index], _str) = (_str, _text.Lines[_index]);
            IsComplete = false;
        }
    }
}
