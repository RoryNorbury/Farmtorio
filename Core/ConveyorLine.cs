
// this will likely be left as a later performance-related addition
namespace Core;

public class ConveyorLine
{
    private List<Entity> _entities;
    public ConveyorLine(Entity[] entities)
    {
        _entities = new List<Entity>(entities);
    }
    public void Tick(double dt)
    {
        foreach (Entity entity in _entities)
        {
            entity.Tick(dt);
        }
    }
}
