namespace Core;

public class Depot : Entity
{
    public override void Tick(double dt)
    {
        Ticked = true;
    }
    public override EntityID ID => EntityID.Depot;
}