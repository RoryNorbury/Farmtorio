namespace Core;

public class Instance
{
    private List<Entity> _entities;
    private List<EntityLine> _entityLines;
    private string _name = "";
    private int _cycle = 0;
    public Instance()
    {
        _entities = new List<Entity>([]);
        _entityLines = new List<EntityLine>([]);
    }
    public Instance(string filename) : this()
    {
        loadFromFile(filename);
    }
    public void Tick(ref bool shouldExit)
    {
        UntickEntities();
        HandleInput(ref shouldExit);
    }
    private void HandleInput(ref bool shouldExit)
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