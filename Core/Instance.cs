namespace Core;

public class Instance
{
    private List<Entity> _entities;
    private List<EntityLine> _entityLines;
    private string _name = "";
    private int _cycle = 0;
    public bool ShouldExit = false;
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
        UntickEntities();
        HandleInput();
    }
    private void HandleInput()
    {

    }
    public void UntickEntities()
    {
        foreach (Entity entity in _entities)
        {
            entity.Ticked = false;
        }
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