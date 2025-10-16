using SplashKitSDK;
namespace Core;

public enum Orientation
{
    North,
    East,
    South,
    West
}
public abstract class Entity
{
    public Point2D position;
    public Orientation orientation;
    public int textureIndex;

    public abstract void Tick();

}