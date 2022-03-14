using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FakeOS.Software.CLI;

public class MkFile : CliSoftware
{
    public MkFile(List<string> args, Action<string> echo = null, string executionDirectory = null) : base(args, echo, executionDirectory) { }

    protected override void run()
    {
        base.run();
        
        if (args.Count != 1)
        {
            write("[error]: Wrong number of arguments.\nUsage: mkfile [file]");
            return;
        } 
        
        string toMake = args.ElementAt(0);

        if (File.Exists(toMake) || Directory.Exists(toMake))
        {
            write("[error]: Provided entry already exists.");
            return;
        }

        make(toMake);

    }
    
    protected virtual void make(string path)
    {
        var file = File.Create(path);
        file.Close();
    }

    private void help()
    {
        write("Usage: Usage: mkfile [file]\n");
    }
    
    

}