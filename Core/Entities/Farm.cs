namespace Core;

public class Farmer : Entity
{
    public override void Tick(double dt)
    {
        Ticked = true;
    }
    public override string EntityID => "Farmer";
}