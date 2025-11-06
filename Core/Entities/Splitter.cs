namespace Core;

public class Splitter : Entity
{
    public override EntityID ID => EntityID.Splitter;
    public override void Tick(double dt)
    {
        // can't be ticked more than once per frame
        if (Ticked) { return; }
        Ticked = true;
    }
}