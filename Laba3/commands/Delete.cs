namespace Laba3.commands;

class Delete : ICommand
{
    private readonly Text _org;
    private readonly Text _copy;
    private readonly TextFileWorker _fileWorker;

    public Delete(Text org, string path)
    {
        _fileWorker = new TextFileWorker(path);
        _org = org;
        _copy = new Text();
    }

    public bool IsComplete { get; private set; }

    public void Execute()
    {
        if (!IsComplete)
        {
            _fileWorker.Write(_copy);

            Text.Swap(_copy, _org);

            IsComplete = true;
        }
    }

    public void Undo()
    {
        if (IsComplete)
        {
            _fileWorker.Write(_org);

            Text.Swap(_copy, _org);

            IsComplete = false;
        }
    }
}
