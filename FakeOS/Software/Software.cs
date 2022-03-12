using System;
using System.Collections.Generic;

namespace FakeOS.Software;

public abstract class Software
{
    protected Software(List<string> args, Action<string> echo)
    {
        
    }
    
    protected string fancyName;
    protected string icon;


}