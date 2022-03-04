using System.Collections.Generic;
using System.IO;

namespace FakeOS.Software.CLI;

public class MkDir : MkFile
{
    public MkDir(List<string> args) : base(args) { }

    protected override void make(string path)
    {
        Directory.CreateDirectory(path);
    }
}