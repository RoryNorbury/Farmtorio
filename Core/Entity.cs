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
    public abstract void Tick();

}