namespace Core;

public class Instance
{
    // TODO: keep this sorted, so I can binary search it
    private List<Entity> _entities;
    private List<ConveyorLine> _conveyorLines;
    private string _name = "";
    private int _cycle = 0;
    public bool ShouldExit = false;
    public Instance()
    {
        _entities = new List<Entity>([]);
        _conveyorLines = new List<ConveyorLine>([]);
    }
    public Instance(string filename) : this()
    {
        loadFromFile(filename);
    }
    public void Tick(double dt)
    {
        UntickEntities();
        TickEntities(dt);
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
    public void TickEntities(double dt)
    {
        foreach (Entity entity in _entities)
        {
            entity.Tick(dt);
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