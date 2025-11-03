namespace Core;

public class Loader : Entity
{
    public override void Tick(double dt)
    {
        Ticked = true;
    }
    public override EntityID ID => EntityID.Loader;
}