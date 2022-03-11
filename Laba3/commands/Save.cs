namespace Laba3.commands;

class Save : ICommand
{
    private readonly Text _org;
    private readonly Text _copy;
    private readonly TextFileWorker _fileWorker;

    public Save(Text org, string path)
    {
        _fileWorker = new TextFileWorker(path);
        _org = org;
        _copy = _fileWorker.Read();
    }

    public bool IsComplete { get; private set; }

    public void Execute()
    {
        if (!IsComplete)
        {
            _fileWorker.Write(_org);
            IsComplete = true;
        }
    }

    public void Undo()
    {
        if (IsComplete)
        {
            _fileWorker.Write(_copy);
            IsComplete = false;
        }
    }
}
