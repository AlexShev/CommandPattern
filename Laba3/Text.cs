using System.Text;

namespace Laba3;

class Text
{
    public List<string> Lines { get; private set; } = new List<string>();

    public Text(string[] text)
    {
        Lines = new List<string>(text);
    }

    public Text() { }

    public override string ToString()
    {
        return new StringBuilder().AppendJoin('\n', Lines).ToString();
    }

    public int Count => Lines.Count;

    public static void Swap(Text f, Text s)
    {
        (f.Lines, s.Lines) = (s.Lines, f.Lines);
    }
}
