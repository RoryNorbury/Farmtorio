using SplashKitSDK;

namespace Core;

public enum MenuID
{
    MainMenu,
    SelectInstanceMenu,
    InstanceEscapeMenu,
    SaveInstanceMenu,
    Instance
}
public abstract class Menu
{
    public Menu() { }
    public Color BackgroundColour = SplashKit.ColorWhite();
    public abstract MenuID ID { get; } // This might not be needed anymore
    public bool ShouldExit = false;
    public abstract void Draw();
    public abstract void HandleInput();
}