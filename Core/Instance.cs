namespace Core;

using SplashKitSDK;

public class Instance
{
    // TODO: keep this sorted, so I can binary search it
    private List<Entity> _entities;
    private List<ConveyorLine> _conveyorLines;
    public string _name = "";
    public string Name {get => _name; set => _name = value;}
    private int _cycle = 0;
    public int Cycle {get => _cycle;}
    public bool ShouldExit = false;
    private EntityID? cursorEntityID = null;
    public Instance()
    {
        _entities = new List<Entity>([]);
        _conveyorLines = new List<ConveyorLine>([]);
    }
    public Instance(string filename) : this()
    {
        LoadFromFile(filename);
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
        // will open escape menu if escape key pressed
        if (SplashKit.KeyTyped(KeyCode.EscapeKey))
        {
            Game.NextMenuID = MenuID.InstanceEscapeMenu;
        }
        if (SplashKit.KeyDown(KeyCode.WKey) || SplashKit.KeyDown(KeyCode.UpKey))
        {
            Camera.Y += Globals.CameraSpeed;
        }
        if (SplashKit.KeyDown(KeyCode.AKey) || SplashKit.KeyDown(KeyCode.LeftKey))
        {
            Camera.X -= Globals.CameraSpeed;
        }
        if (SplashKit.KeyDown(KeyCode.SKey) || SplashKit.KeyDown(KeyCode.DownKey))
        {
            Camera.Y -= Globals.CameraSpeed;
        }
        if (SplashKit.KeyDown(KeyCode.DKey) || SplashKit.KeyDown(KeyCode.RightKey))
        {
            Camera.X += Globals.CameraSpeed;
        }

        // allows the selection of entities to place
        for (int i = 0; i <= (int)EntityID.Depot; i++)
        {
            if (SplashKit.KeyTyped((KeyCode)((int)KeyCode.Num0Key + i + 1)))
            {
                cursorEntityID = (EntityID)i;
            }
        }
        if (SplashKit.KeyTyped(KeyCode.Num0Key))
        {
            cursorEntityID = null;
        }

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
    public List<Entity> DrawableEntities()
    {
        List<Entity> ret = new List<Entity>(_entities);
        if (cursorEntityID != null)
        {
            // add a preview entity at the mouse position
            Point2D mousePos = SplashKit.MousePosition();
            double worldX = Math.Floor((mousePos.X - (Globals.WindowWidth - Globals.ZoomScale) / 2) / Globals.ZoomScale) + Camera.X;
            double worldY = -Math.Floor((mousePos.Y - (Globals.WindowHeight - Globals.ZoomScale) / 2) / Globals.ZoomScale) + Camera.Y;
            Point2D entityPos = SplashKit.PointAt(worldX, worldY);
            Entity previewEntity;
            switch (cursorEntityID)
            {
                case EntityID.Conveyor:
                    previewEntity = new Conveyor();
                    break;
                case EntityID.Loader:
                    previewEntity = new Loader();
                    break;
                case EntityID.Splitter:
                    previewEntity = new Splitter();
                    break;
                case EntityID.Manufactory:
                    previewEntity = new Manufactory();
                    break;
                case EntityID.Farm:
                    previewEntity = new Farmer();
                    break;
                case EntityID.Depot:
                    previewEntity = new Depot();
                    break;
                default:
                    throw new Exception("Invalid cursor entity ID");
            }
            previewEntity.Position = entityPos;
            ret.Add(previewEntity);
        }
        return ret;
    }
    public void AddEntity(Entity entity)
    {
        _entities.Add(entity);
    }
    public void LoadFromFile(string filename)
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
                        // replace with another method to ensure it is consistent with EntityID enum
                        case "Conveyor": entity = new Conveyor(); break;
                        case "Loader": entity = new Loader(); break;
                        case "Splitter": entity = new Splitter(); break;
                        case "Manufactory": entity = new Manufactory(); break;
                        case "Farm": entity = new Farmer(); break;
                        case "Depot": entity = new Depot(); break;
                        default: throw new Exception($"Cannot load entity: Unknown entityID: '{entityID}'");
                    }
                    try
                    {
                        entity.LoadFromData(data);
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Cannot load entity of type '{entityID}': {e.Message}");
                    }
                    _entities.Add(entity);
                }
            }
            Camera.X = 0;
            Camera.Y = 0;
        }
        catch (Exception e)
        {
            throw new Exception("Could not load instance from file: " + e.Message);
        }
    }
    public void SaveToFile(string filename)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(_name + "," + _cycle.ToString());
                foreach (Entity entity in _entities)
                {
                    List<string> data = entity.GetSaveData();
                    data.Insert(0, Entity.EntityIDStrings[(int)entity.ID]);
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