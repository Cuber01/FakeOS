using System.Collections.Generic;

namespace FakeOS.Software.CLI;

public abstract class CliSoftware
{
    protected List<string> args;

    protected CliSoftware(List<string> args)
    {
        this.args = args;
        
        run();
    }
    
    protected abstract void run();

    protected void write(string message)
    {
        // TODO
    }
}