using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FakeOS.Software.CLI;

public class Mv : Cp
{
    private readonly List<string> thingsToMove = new List<string>();
    public Mv(List<string> args) : base(args) { }

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

        gatherInfo();
        copy();

    }

    protected override void copyFile(string sourceDir, string destinationDir)
    {
        base.copyFile(sourceDir, destinationDir);

        if (File.Exists(sourceDir))
        {
            File.Delete(sourceDir);    
        }
        else
        {
            Directory.Delete(sourceDir!, true);
        }
            
    }

    protected override void handleFlags()
    {
        flags.Add("-r", false);

        base.handleFlags();
    }

    private void help()
    {
        write("Usage: Usage: mv [optional flags] [targets] [output]\n");
        
        write("Flags:");
        write("     -r  Recursively move a directory");
    }
    
    

}