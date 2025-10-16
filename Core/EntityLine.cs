namespace Core;

public class EntityLine
{
    private List<Entity> _entities;
    public EntityLine(Entity[] entities)
    {
        _entities = new List<Entity>(entities);
    }
    public void Tick()
    {
        foreach (Entity entity in _entities)
        {
            entity.Tick();
        }
    }
}
