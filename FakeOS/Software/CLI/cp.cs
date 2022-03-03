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

        gatherInfo();
        copy();

    }

    private void gatherInfo()
    {
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
    }

    private void copy()
    {
        string lastArg = argsWithoutFlags.ElementAt(argsWithoutFlags.Count - 1);
        
        if (thingsToCopy.Count == 1)
        {

            if (Directory.Exists(lastArg))
            {
                copyDirectory(thingsToCopy.ElementAt(0), lastArg!, true);
            } 
            else
            {
                File.Copy(thingsToCopy.ElementAt(0), lastArg!);
            }
        }
        else
        {
            foreach (var entry in thingsToCopy)
            {
                File.Copy(entry, lastArg);
            }
        }
    }
    
    private void copyDirectory(string sourceDir, string destinationDir, bool recursive)
    {
        // Get information about the source directory
        var dir = new DirectoryInfo(sourceDir);

        // Check if the source directory exists
        if (!dir.Exists)
            throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

        // Cache directories before we start copying
        DirectoryInfo[] dirs = dir.GetDirectories();

        // Create the destination directory
        Directory.CreateDirectory(destinationDir);

        // Get the files in the source directory and copy to the destination directory
        foreach (FileInfo file in dir.GetFiles())
        {
            string targetFilePath = Path.Combine(destinationDir, file.Name);
            file.CopyTo(targetFilePath);
        }

        // If recursive and copying subdirectories, recursively call this method
        if (recursive)
        {
            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                copyDirectory(subDir.FullName, newDestinationDir, true);
            }
        }
    }


    
    
    protected override void handleFlags()
    {
        flags.Add("-r", false);

        base.handleFlags();
    }

    private void help()
    {
        write("Usage: Usage: cp [optional flags] [targets] [output]\n");
        
        write("Flags:");
        write("     -r  Recursively copy a directory");
    }
    
    

}