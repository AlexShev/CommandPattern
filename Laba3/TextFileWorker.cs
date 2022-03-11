namespace Laba3;

class TextFileWorker
{
    private readonly string _path;
    public TextFileWorker(string path)
    {
        _path = path;
    }

    public Text Read()
    {
        return new Text(File.ReadAllLines(_path));
    }

    public void Write(Text text)
    {
        using (StreamWriter outStream = new(_path))
        {
            List<string> lines = text.Lines;

            foreach (string line in lines)
                outStream.WriteLine(line);
        }
    }
}
