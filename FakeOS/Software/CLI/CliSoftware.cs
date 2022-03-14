using System;
using System.Collections.Generic;
using System.Linq;

namespace FakeOS.Software.CLI;

public abstract class CliSoftware : Software
{
    protected readonly List<string> args;
    protected readonly Action<string> echo;
    protected readonly string executionDirectory;
    protected List<string> argsWithoutFlags;
    
    protected Dictionary<string, bool> flags = new Dictionary<string, bool>();

    protected CliSoftware(List<string> args, Action<string> echo = null, string executionDirectory = null) : base(args, echo, executionDirectory)
    {
        this.args = args;
        this.echo = echo;
        this.executionDirectory = executionDirectory;
        
        run();
    }

    private void setFlags()
    {
        for(int i = 0; i < flags.Count; i++)
        {
            string flagKey = flags.Keys.ElementAt(i);

            if (args.Contains(flagKey))
            {
                flags[flagKey] = true;
            } 
        }
    }

    private void generateArgsWithoutFlags()
    {
        argsWithoutFlags = new List<string>();
        
        foreach (var arg in args)
        {
            if(flags.ContainsKey(arg)) continue;
            
            argsWithoutFlags.Add(executionDirectory != null ? executionDirectory+arg : arg);
        }
    }
    
    protected void write(string message)
    {
        if (echo is null)
        {
            Console.WriteLine(message);    
        }
        else
        {
            echo.Invoke(message);
        }
    }

    protected virtual void run()
    {
        handleFlags();
        generateArgsWithoutFlags();
    }

    protected virtual void handleFlags()
    {
        // Add your flags to dictionary
        
        setFlags();
    }

}