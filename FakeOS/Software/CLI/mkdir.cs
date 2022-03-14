using System;
using System.Collections.Generic;
using System.IO;

namespace FakeOS.Software.CLI;

public class MkDir : MkFile
{
    public MkDir(List<string> args, Action<string> echo = null, string executionDirectory = null) : base(args, echo, executionDirectory) { }

    protected override void make(string path)
    {
        Directory.CreateDirectory(path);
    }
    
    protected override void help()
    {
        write("Usage: Usage: mkdir [dir]");
    }

}