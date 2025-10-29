using SplashKitSDK;
namespace Core;

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
    public int TextureIndex;
    public bool Ticked = false;
    public abstract string EntityID { get; }
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
        return new List<string>([Position.X.ToString(), Position.Y.ToString(), Orientation.ToString(), TextureIndex.ToString()]);
    }
    // unused data should be stripped by child class before calling this
    public virtual void LoadFromData(List<string> data)
    {
        int i = data.Count - 4;
        Position.X = double.Parse(data[i].Replace(".", ","));
        i++;
        Position.X = double.Parse(data[i].Replace(".", ","));
        i++;
        Orientation = (OrientationID)int.Parse(data[i]);
        i++;
        TextureIndex = int.Parse(data[i]);
    }
}