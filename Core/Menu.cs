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
    public Color BackgroundColour = SplashKit.ColorWhite();
    public abstract int ID { get; }
    public bool ShouldExit = false;
    public MenuID NextMenuID; // this is bad, should enforce declaring in overriding classes. Possibly make a property
    public abstract void Draw();
    public abstract void HandleInput();
}