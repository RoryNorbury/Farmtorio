namespace Core;

using SplashKitSDK;

public class Instance
{
    // TODO: make camera class
    // TODO: Add functions for converting between screen and world coordinates
    // TODO: keep this sorted, so I can binary search it
    private List<Entity> _entities;
    private List<ConveyorLine> _conveyorLines;
    public string _name = "";
    public string Name { get => _name; set => _name = value; }
    private int _cycle = 0;
    public int Cycle { get => _cycle; }
    public bool ShouldExit = false;
    private EntityID? previewEntityID = null;
    private OrientationID previewOrientation = OrientationID.East;
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
            // also reset preview entity
            previewEntityID = null;
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

        // allows the selection of an entity to place
        for (int i = 0; i <= (int)EntityID.Depot; i++)
        {
            if (SplashKit.KeyTyped((KeyCode)((int)KeyCode.Num0Key + i + 1)))
            {
                previewEntityID = (EntityID)i;
            }
        }
        // clears the selected placement entity
        if (SplashKit.KeyTyped(KeyCode.Num0Key))
        {
            previewEntityID = null;
        }
        // rotates the preview entity
        if (SplashKit.KeyTyped(KeyCode.RKey) && previewEntityID != null && EntityFactory.CreateEmptyEntity(previewEntityID.Value).isDirectional)
        {
            previewOrientation = (OrientationID)(((int)previewOrientation + 1) % 4);
        }
        // places the selected entity at the mouse position
        if (SplashKit.MouseDown(MouseButton.LeftButton) && previewEntityID != null)
        {
            // place entity at mouse position
            Point2D entityPos = GridCoordinates(ScreenToWorldCoords(SplashKit.MousePosition()));
            Entity newEntity = EntityFactory.CreateEmptyEntity(previewEntityID.Value);
            newEntity.Position = entityPos;
            newEntity.Orientation = previewOrientation;
            AddEntity(newEntity); // could check if this succeeds or not, but not currently necessary
        }
        // removes entity at mouse position
        if (SplashKit.MouseDown(MouseButton.RightButton))
        {
            Point2D worldPos = GridCoordinates(ScreenToWorldCoords(SplashKit.MousePosition()));
            Entity? entityToRemove = GetEntityAtPosition(worldPos);
            if (entityToRemove != null)
            {
                _entities.Remove(entityToRemove);
            }
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
        if (previewEntityID != null)
        {
            // add a preview entity at the mouse position
            Point2D entityPos = GridCoordinates(ScreenToWorldCoords(SplashKit.MousePosition()));
            Entity previewEntity = EntityFactory.CreateEmptyEntity(previewEntityID.Value);
            previewEntity.Position = entityPos;
            previewEntity.Orientation = previewOrientation;
            ret.Add(previewEntity);
        }
        return ret;
    }
    // consider adding a way for checking if the entity was added successfully
    public void AddEntity(Entity entity)
    {
        // check position isn't already occupied
        if (GetEntityAtPosition(entity.Position) != null)
        {
            return;
        }
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
                    // potentially use an enumeration, but using a string makes the save more readable
                    Entity entity = EntityFactory.CreateEmptyEntity(entityID);
                    try
                    {
                        entity.LoadFromData(data);
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Cannot load entity of type '{entityID}': {e.Message}");
                    }
                    AddEntity(entity);
                }
            }
            Camera.X = 0;
            Camera.Y = 0;
            // this should be done for each entity after it is placed or an entity is placed around it
            UpdateNextEntities();
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
                foreach (Entity e in _entities)
                {
                    List<string> data = e.GetSaveData();
                    data.Insert(0, Entity.EntityIDStrings[(int)e.ID]);
                    writer.WriteLine(string.Join(",", data));
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception("Could not save instance to file: " + e.Message);
        }
    }
    public static Point2D ScreenToWorldCoords(Point2D screenPos)
    {
        // undo centering of coordianates on screen
        double x = screenPos.X - Globals.WindowWidth / 2;
        double y = screenPos.Y - Globals.WindowHeight / 2;
        // undo zoom so that coordinates are in world units
        x /= Globals.ZoomScale;
        y /= -Globals.ZoomScale;
        // undo camera offset
        x += Camera.X;
        y += Camera.Y;
        // offset by half a world unit
        x += 0.5;
        y += 0.5;
        // y is inverted on screen
        return new Point2D() { X = x, Y = y };
    }
    public static Point2D WorldToScreenCoords(Point2D worldPos)
    {
        // offset coordinates by camera position
        double x = worldPos.X - Camera.X;
        double y = worldPos.Y - Camera.Y;
        // offset by half a world unit
        x -= 0.5;
        y += 0.5;
        // apply zoom so that coordinates are in screen units
        x *= Globals.ZoomScale;
        y *= Globals.ZoomScale;
        // center coordinates on screen so that 0, 0 is in the middle of the screen (when camera is at origin)
        x = x + Globals.WindowWidth / 2;
        y = -y + Globals.WindowHeight / 2;
        // y is inverted on screen
        return new Point2D() { X = x, Y = y };
    }
    public static Point2D GridCoordinates(Point2D worldPos)
    {
        return new Point2D()
        {
            X = Math.Floor(worldPos.X),
            Y = Math.Floor(worldPos.Y)
        };
    }
    public Entity? GetEntityAtPosition(Point2D worldPosition)
    // TODO: optimize this search
    /*
        Ideas:
        - store entities in a sorted list based on position (rows by row)
        - use chunks
        - use a dictionary with position as key (i think this would only be faster for large numbers of entities)
    */
    {
        foreach (Entity entity in _entities)
        {
            // compare integer values to avoid floating point errors
            // actually this is the same as flooring both values, idk which i prefer
            if ((int)entity.Position.X == (int)worldPosition.X && (int)entity.Position.Y == (int)worldPosition.Y)
            {
                return entity;
            }
        }
        return null;
    }
    public void UpdateNextEntities()
    {
        // reset all next entities
        foreach (Entity entity in _entities)
        {
            if (entity is Conveyor conveyor)
            {
                conveyor.NextEntity = null;
            }
            if (entity is Loader loader)
            {
                loader.NextEntity = null;
            }
        }
        foreach (Entity entity in _entities)
        {
            // conveyors
            if (entity is Conveyor conveyor)
            {
                // find entity in front of this one
                Point2D nextPos = new Point2D() { X = conveyor.Position.X, Y = conveyor.Position.Y };
                switch (conveyor.Orientation)
                {
                    case OrientationID.North:
                        nextPos.Y += 1;
                        break;
                    case OrientationID.East:
                        nextPos.X += 1;
                        break;
                    case OrientationID.South:
                        nextPos.Y -= 1;
                        break;
                    case OrientationID.West:
                        nextPos.X -= 1;
                        break;
                }
                // assign next entity to the one at this position (null is properly handled)
                conveyor.NextEntity = GetEntityAtPosition(nextPos);
            }
            if (entity is Loader loader)
            {
                // find entities in front of and behind this one
                Point2D nextPos = new Point2D() { X = loader.Position.X, Y = loader.Position.Y };
                Point2D prevPos = new Point2D() { X = loader.Position.X, Y = loader.Position.Y };
                switch (loader.Orientation)
                {
                    case OrientationID.North:
                        nextPos.Y += 1;
                        prevPos.Y -= 1;
                        break;
                    case OrientationID.East:
                        nextPos.X += 1;
                        prevPos.X -= 1;
                        break;
                    case OrientationID.South:
                        nextPos.Y -= 1;
                        prevPos.Y += 1;
                        break;
                    case OrientationID.West:
                        nextPos.X -= 1;
                        prevPos.X += 1;
                        break;
                }
                // assign next entity to the one at this position (null is properly handled)
                loader.NextEntity = GetEntityAtPosition(nextPos);
                loader.PreviousEntity = GetEntityAtPosition(prevPos);
            }
        }
    }
}