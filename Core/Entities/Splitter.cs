namespace Core;

public class Splitter : Entity
{
    public override void Tick(double dt)
    {
        Ticked = true;
    }
    public override string EntityID => "Splitter";
}