namespace Core;

public class Farmer : Entity
{
    public override void Tick(double dt)
    {
        Ticked = true;
    }
    public override EntityID ID => EntityID.Farm;
    public override bool isDirectional => false;

}