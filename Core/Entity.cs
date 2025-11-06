using SplashKitSDK;
namespace Core;

public enum EntityID
{
    Conveyor,
    Loader,
    Router,
    Manufactory,
    Farm,
    Depot
}



public enum OrientationID
{
    North,
    East,
    South,
    West
}
public abstract class Entity
{
    public Point2D Position;
    public OrientationID Orientation;
    public virtual bool isDirectional => true;
    public int TextureIndex; // not used anymore, delete when you get time
    public bool Ticked = false;
    public abstract EntityID ID { get; }
    public abstract void Tick(double dt);
    public Entity() {}
    public Entity(Point2D position, OrientationID orientation, int textureIndex)
    {
        Position = position;
        Orientation = orientation;
        TextureIndex = textureIndex;
    }
    public virtual List<string> GetSaveData()
    {
        return new List<string>([Position.X.ToString(), Position.Y.ToString(), ((int)Orientation).ToString(), TextureIndex.ToString()]);
    }
    // unused data should be stripped by child class before calling this
    public virtual void LoadFromData(List<string> data)
    {
        // could change to < for future proofing
        if (data.Count != 4)
        {
            throw new Exception("Class 'Entity Base' requires 4 entries, " + data.Count + " provided.");
        }
        int i = 0;
        Position.X = double.Parse(data[i].Replace(".", ","));
        i++;
        Position.Y = double.Parse(data[i].Replace(".", ","));
        i++;
        Orientation = (OrientationID)int.Parse(data[i]);
        i++;
        TextureIndex = int.Parse(data[i]);
    }

    // need a way to keep this and the id's coupled, or remove the need for both
    public static string[] EntityIDStrings =
    [
        "Conveyor",
        "Loader",
        "Router",
        "Manufactory",
        "Farm",
        "Depot",
    ];
}