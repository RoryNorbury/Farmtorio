using SplashKitSDK;

namespace Core;

public enum MenuID
{
    MainMenu,
    SelectInstanceMenu,
    InstanceEscapeMenu,
    Instance
}
public abstract class Menu
{
    public Menu() { }
    public Color BackgroundColour = SplashKit.ColorWhite();
    public abstract int ID { get; }
    public bool ShouldExit = false;
    public abstract void Draw();
    public abstract void HandleInput();
}