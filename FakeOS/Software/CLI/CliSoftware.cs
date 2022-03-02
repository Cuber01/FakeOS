using System;
using System.Collections.Generic;
using System.Linq;

namespace FakeOS.Software.CLI;

public abstract class CliSoftware
{
    protected List<string> args;
    protected Dictionary<string, bool> flags = new Dictionary<string, bool>();

    protected CliSoftware(List<string> args)
    {
        this.args = args;
        
        run();
    }

    private void setFlags()
    {
        for(int i = 0; i < args.Count; i++)
        {
            string flagKey = flags.Keys.ElementAt(i);
            
            if (args.Contains(flagKey))
            {
                flags[flagKey] = true;
            } 
        }
    }
    
    protected void write(string message)
    {
        // TODO
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