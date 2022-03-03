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

    private List<string> thingsToCopy = new List<string>();

    public Cp(List<string> args) : base(args) { }

    protected override void run()
    {
        base.run();
        generateArgsWithoutFlags();
        
        // Check if there are any args
        if (args.Count <= 0)
        {
            write("Error: Missing operand.\n For usage run cp --help");
            return;
        }

        if (argsWithoutFlags.Count < 2)
        {
            write("Error: Too few arguments.");
            return;
        }

        for(int i = 0; i < argsWithoutFlags.Count; i++)
        {
            var arg = args.ElementAt(i);

            if (i == 0 && !flags["-r"] && Directory.Exists(arg))
            {
                write("Error: Can't copy directory. Run with -r to copy the whole directory.");
                return;
            }
            
            if (File.Exists(arg))
            {
                thingsToCopy.Add(arg);
            }
            else if (flags["-r"] && Directory.Exists(arg))
            {
                thingsToCopy.Add(arg);
            }
        }

        if (thingsToCopy.Count == 1)
        {
            string lastArg = argsWithoutFlags.ElementAt(argsWithoutFlags.Count - 1);
            
            if (Directory.Exists(lastArg))
            {
                // copy file to dir
            } 
            else
            {
                // copy file with a new name
            }
        }
        else
        {
            // enumerate and copy to the last directory
        }
        

    }
    
    protected override void handleFlags()
    {
        flags.Add("-r", false);
        flags.Add("?isTargetFile", false);

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
        write("Usage: Usage: cp [optional flags] [targets] [output]\n");
        
        write("Flags:");
        write("     -r  Recursively copy a directory");
    }
    
    

}