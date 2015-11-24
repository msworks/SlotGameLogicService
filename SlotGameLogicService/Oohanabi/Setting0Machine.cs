using System.Collections.Generic;
using System.Linq;

public enum DrawSetting0
{
    Normal,
    Hazure
}

public class Setting0Machine
{
    List<DrawSetting0> list;
    IEnumerator<DrawSetting0> enumerator;

    public Setting0Machine()
    {
        var h1 = Enumerable.Range(0, 16).Select(i => DrawSetting0.Normal);
        var h2 = new[] { DrawSetting0.Hazure };

        list = h1.Concat(h2).ToList();

        enumerator = list.GetEnumerator();
    }

    public DrawSetting0 Draw()
    {
        var result = enumerator.Current;

        if (enumerator.MoveNext() == false)
        {
            enumerator.Reset();
        }

        return result;
    }
}

