using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FakeOS.Software.CLI;

public class Cp : CliSoftware
{
    // TODO
    // copy multiple files : [file] [file] [dir]
    // copy file : [file] [new filename]
    // copy file to dir : [file] [dir]
    // copy directory : -r [dir] [new dirname]
    // copy directory : -r [dir] [dir]
    
    public Cp(List<string> args) : base(args) { }

    protected override void run()
    {
        base.run();
        
        // Check if there are any args
        if (args.Count <= 0)
        {
            write("Error: Missing operand.\n For usage run cp --help");
            return;
        }
        
        // Last arg is always the target
        string toRemove = args.ElementAt(args.Count - 1);

        if (!(File.Exists(toRemove) || Directory.Exists(toRemove)))
        {
            write("Error: Provided path does not exist.");
            return;
        }
        
        if(flags["-r"] is false && Directory.Exists(toRemove))
        {
            write("Error: Provided path is a directory. Run with -r to recursively remove the whole directory.");
            return;
        }

        delete(toRemove);

    }
    
    protected override void handleFlags()
    {
        flags.Add("-r", false);
        flags.Add(null!, false);
        
        base.handleFlags();
    }
    
    private void delete(string path)
    {
        if (flags["-r"])
        {
            Directory.Delete(path, true);
        }
        else
        {
            File.Delete(path);
        }
    }

    private void help()
    {
        write("Usage: Usage: rm [optional flags] [file]\n");
        
        write("Flags:");
        write("     -r  Recursively remove a directory");
    }
    
    

}