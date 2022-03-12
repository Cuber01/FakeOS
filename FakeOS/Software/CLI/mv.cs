using System;
using System.Collections.Generic;
using System.IO;

namespace FakeOS.Software.CLI;

public class Mv : Cp
{
    private readonly List<string> thingsToMove = new List<string>();
    public Mv(List<string> args, Action<string> echo) : base(args, echo) { }

    protected override void run()
    {
        handleFlags();
        generateArgsWithoutFlags();
        
        // Check if there are any args
        if (args.Count <= 0)
        {
            write("Error: Missing operand.\nFor usage run cp --help");
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
        
        File.Delete(sourceDir); 
    }

    protected override void copyDirectory(string sourceDir, string destinationDir, bool recursive)
    {
        base.copyDirectory(sourceDir, destinationDir, recursive);
        
        Directory.Delete(sourceDir);
    }

    private void help()
    {
        write("Usage: Usage: mv [optional flags] [targets] [output]\n");
        
        write("Flags:");
        write("     -r  Recursively move a directory");
    }
    
    

}