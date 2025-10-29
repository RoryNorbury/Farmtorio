namespace Core;

public class Loader : Entity
{
    public override void Tick(double dt)
    {
        Ticked = true;
    }
    public override string EntityID => "Loader";
}