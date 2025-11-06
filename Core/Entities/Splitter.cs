namespace Core;

public class Router : Entity
{
    public override EntityID ID => EntityID.Router;
    public override void Tick(double dt)
    {
        // can't be ticked more than once per frame
        if (Ticked) { return; }
        Ticked = true;
    }
}