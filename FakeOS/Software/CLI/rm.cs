using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FakeOS.Software.CLI;

public class Rm : CliSoftware
{
    public Rm(List<string> args, Action<string> echo = null, string executionDirectory = null) : base(args, echo, executionDirectory) { }

    protected override void run()
    {
        base.run();

        if (args.Contains("--help"))
        {
            help();
            return;
        }
        
        // Check if there are any args
        if (args.Count <= 0)
        {
            write("[error]: Missing operand.\nUsage: rm [optional flags] [file]");
            return;
        }
        
        // Last arg is always the target
        string toRemove = argsWithoutFlags.ElementAt(argsWithoutFlags.Count - 1);

        if (!File.Exists(toRemove) && !Directory.Exists(toRemove))
        {
            write("[error]: Provided path does not exist.");
            return;
        }
        
        if(flags["-r"] is false && Directory.Exists(toRemove))
        {
            write("[error]: Provided path is a directory. Run with -r to recursively remove the whole directory.");
            return;
        }

        delete(toRemove);

    }
    
    protected override void handleFlags()
    {
        flags.Add("-r", false);
        
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