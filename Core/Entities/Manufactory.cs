namespace Core;

public class Manufactory : Entity
{
    public override void Tick(double dt)
    {
        Ticked = true;
    }
    public override string EntityID => "Manufactory";
}