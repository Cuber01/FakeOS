using System;
using System.Collections.Generic;
using System.Linq;

namespace FakeOS.Software.CLI;

public abstract class CliSoftware : Software
{
    protected readonly List<string> args;
    protected List<string> argsWithoutFlags;
    
    protected Dictionary<string, bool> flags = new Dictionary<string, bool>();

    // If a key in the flags dictionary starts with this, it will not be possible to change it via a flag input through terminal by user
    private const char internalFlagMark = '?';

    protected CliSoftware(List<string> args) : base(args)
    {
        this.args = args;
        
        run();
    }

    private void setFlags()
    {
        for(int i = 0; i < flags.Count; i++)
        {
            string flagKey = flags.Keys.ElementAt(i);
            
            // Internal flags are set by the program itself
            if(flagKey.StartsWith(internalFlagMark)) continue;
            
            if (args.Contains(flagKey))
            {
                flags[flagKey] = true;
            } 
        }
    }

    protected void generateArgsWithoutFlags()
    {
        argsWithoutFlags = new List<string>();
        
        foreach (var arg in args)
        {
            if(flags.ContainsKey(arg)) continue;
            
            argsWithoutFlags.Add(arg);
        }
    }
    
    protected void write(string message)
    {
        // TODO Redirect this to console later on
        Console.WriteLine(message);
    }

    protected virtual void run()
    {
        handleFlags();
    }

    protected virtual void handleFlags()
    {
        // Add your flags to dictionary
        
        setFlags();
    }

}