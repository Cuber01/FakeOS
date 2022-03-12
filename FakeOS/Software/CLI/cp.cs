using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FakeOS.Software.CLI;

public class Cp : CliSoftware
{
    // copy multiple files : [file] [file] [dir]
    // copy file : [file] [new filename]
    // copy file to dir : [file] [dir]
    // copy directory : -r [dir] [new dirname]
    // copy directory : -r [dir] [dir]

    private readonly List<string> thingsToCopy = new List<string>();

    public Cp(List<string> args, Action<string> echo) : base(args, echo) { }

    protected override void run()
    {
        base.run();
        generateArgsWithoutFlags();
        
        // Check if there are any args
        if (args.Count <= 0)
        {
            write("[error]: Missing operand.\nFor usage run cp --help");
            return;
        }

        if (argsWithoutFlags.Count < 2)
        {
            write("[error]: Too few arguments.");
            return;
        }

        gatherInfo();
        copy();

    }

    protected void gatherInfo()
    {
        for(int i = 0; i < argsWithoutFlags.Count; i++)
        {
            var arg = argsWithoutFlags.ElementAt(i);

            // Only last arg can be a directory, unless we're running with -r
            if (i != argsWithoutFlags.Count - 1 && !flags["-r"] && Directory.Exists(arg))
            {
                write("[error]: Can't copy directory. Run with -r to copy the whole directory.");
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

    protected void copy()
    {
        string lastArg = argsWithoutFlags.ElementAt(argsWithoutFlags.Count - 1);

        // try
        // {

            if (thingsToCopy.Count == 1)
            {
                string toCopy = thingsToCopy.ElementAt(0);

                // [dir] [dir]
                if (Directory.Exists(toCopy))
                {
                    copyDirectory(toCopy, lastArg!, true);
                }
                // [file] [dir] / [file] [new filename]
                else
                {
                    copyFile(toCopy!, lastArg!);
                }
            }
            else
            {
                // [files] [dir] / [dirs] [dir]
                foreach (var entry in thingsToCopy)
                {
                    if (File.Exists(entry))
                    {
                        copyFile(entry, lastArg);
                    }
                    else if (Directory.Exists(entry))
                    {
                        copyDirectory(entry, lastArg, true);
                    }

                }
            }
        //
        // }
        // catch (IOException)
        // {
        //     write("[error]: An entry with this name already exists.");
        // }
    }

    protected virtual void copyFile(string sourceDir, string destinationDir)
    {
        try
        {
            File.Copy(sourceDir, destinationDir);
        }
        catch (Exception e)
        {
            write(e.Message);
        }
    }
    
    protected virtual void copyDirectory(string sourceDir, string destinationDir, bool recursive)
    {
        // Get information about the source directory
        var dir = new DirectoryInfo(sourceDir);

        // Check if the source directory exists
        if (!dir.Exists)
        {
            write("[error]: Source directory not found: {dir.FullName}");
            return;
        }

        // Cache directories before we start copying
        DirectoryInfo[] dirs = dir.GetDirectories();

        // Create the destination directory
        Directory.CreateDirectory(destinationDir);

        // Get the files in the source directory and copy to the destination directory
        foreach (FileInfo file in dir.GetFiles())
        {
            string targetFilePath = Path.Combine(destinationDir, file.Name);
            copyFile(file.FullName, targetFilePath);
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