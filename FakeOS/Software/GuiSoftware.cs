namespace FakeOS.Software;

public abstract class GuiSoftware
{
    protected string name;
    protected bool running;

    public virtual void update() {}
    
    public abstract void draw();
}