using System;
using System.Collections.Generic;

namespace FakeOS.Software.GUI;

public abstract class GuiSoftware : Software
{
    protected GuiSoftware(List<string> args, Action<string> echo = null, string executionDirectory = null) : base(args, echo, executionDirectory)
    {
    }
    
    public bool running = true;

    public virtual void update() {}
    public virtual void imGuiUpdate() {}

}