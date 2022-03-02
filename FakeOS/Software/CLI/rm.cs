using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace FakeOS.Software.CLI;

public class Rm : CliSoftware
{
    public Rm(List<string> args) : base(args) { }
    
    protected override void run()
    {
        if (args.Count <= 0)
        {
            write("Error: Missing operand.\n Usage: rm [optional flags] [file]");
            return;
        }
        
        string toRemove = args.ElementAt(args.Count - 1);

        if (!(File.Exists(toRemove) || Directory.Exists(toRemove)))
        {
            write("Error: Provided path does not exist.");
            return;
        }
        
        if(Directory.Exists(toRemove))
        {
            write("Error: Provided path is a directory. Run with -r to recursively remove the whole directory.");
            return;
        }

        delete(toRemove);

    }

    private void delete(string path)
    {
        File.Delete(path);
    }
    
    

}