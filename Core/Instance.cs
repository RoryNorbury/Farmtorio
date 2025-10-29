namespace Core;

public class Instance
{
    // TODO: keep this sorted, so I can binary search it
    private List<Entity> _entities;
    private List<ConveyorLine> _conveyorLines;
    public string _name = "";
    public string Name {get => _name;}
    private int _cycle = 0;
    public int Cycle {get => _cycle;}
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
        _cycle++;
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
    public void AddEntity(Entity entity)
    {
        _entities.Add(entity);
    }
    public void loadFromFile(string filename)
    {
        try
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                if (reader.EndOfStream)
                {
                    throw new Exception("File is empty");
                }
                // read instance info
                string[] temp = reader.ReadLine().Split(',');
                if (temp.Length != 2)
                {
                    throw new Exception("Invalid instance data");
                }
                else
                {
                    _name = temp[0];
                    _cycle = int.Parse(temp[1]);
                }
                // read entity info, line by line
                while (!reader.EndOfStream)
                {
                    List<string> data = reader.ReadLine().Split(',').ToList();
                    string entityID = data[0];
                    data.RemoveAt(0);
                    // potentially use an enumeration, but this makes the save more readable
                    Entity entity;
                    switch (entityID)
                    {
                        case "loader": entity = new Loader(); break;
                        case "Manufactory": entity = new Manufactory(); break;
                        case "Farmer": entity = new Farmer(); break;
                        case "Conveyor": entity = new Conveyor(); break;
                        case "Loader": entity = new Loader(); break;
                        case "Splitter": entity = new Splitter(); break;
                        default: throw new Exception("Cannot load entity: Unknown entityID: " + entityID);
                    }
                    try
                    {
                        entity.LoadFromData(data);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Cannot load entity: " + e.Message);
                    }
                    _entities.Add(entity);
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception("Could not load instance from file: " + e.Message);
        }
    }
    public void saveToFile(string filename)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(_name + "," + _cycle.ToString());
                foreach (Entity entity in _entities)
                {
                    List<string> data = entity.GetSaveData();
                    data.Insert(0, entity.EntityID);
                    writer.WriteLine(string.Join(",", data));
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception("Could not save instance to file: " + e.Message);
        }
    }
}