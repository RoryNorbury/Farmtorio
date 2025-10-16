namespace Core;

public class Instance
{
    private List<Entity> _entities;
    private List<EntityLine> _entityLines;
    public Instance()
    {
        _entities = new List<Entity>([]);
        _entityLines = new List<EntityLine>([]);
    }
    public Instance(string filename) : this()
    {
        loadFromFile(filename);
    }
    public void Tick()
    {

    }
    public Entity[] DrawableEntities()
    {
        throw new NotImplementedException();
    }
    public void loadFromFile(string filename)
    {

    }
    public void saveToFile(string filename)
    {
        
    }
}