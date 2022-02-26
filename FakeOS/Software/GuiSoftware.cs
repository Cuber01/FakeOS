namespace FakeOS.Software;

public abstract class GuiSoftware
{
    protected string name;
    public bool running = true;

    // It is very likely, I'll eventually put something in either update or draw, that's why they're virtual
    public virtual void update() { }

    public virtual void draw() { }
}