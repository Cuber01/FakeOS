using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FakeOS.Software.CLI;

public class MkFile : CliSoftware
{
    public MkFile(List<string> args) : base(args) { }

    protected override void run()
    {
        base.run();
        
        if (args.Count != 1)
        {
            write("Error: Wrong number of arguments.\n Usage: mkfile [file]");
            return;
        } 
        
        string toMake = args.ElementAt(0);

        if (File.Exists(toMake) || Directory.Exists(toMake))
        {
            write("Error: Provided entry already exists.");
            return;
        }

        make(toMake);

    }
    
    protected virtual void make(string path)
    {
        File.Create(path);
    }

    private void help()
    {
        write("Usage: Usage: mkfile [file]\n");
    }
    
    

}