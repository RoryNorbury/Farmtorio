namespace Core;

public abstract class Menu
{
    public abstract int ID { get; }
    public abstract void Draw();
    public abstract void HandleInput();
}