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
    public Menu()
    {
        NextMenuID = (MenuID) ID;
    }
    public Color BackgroundColour = SplashKit.ColorWhite();
    public abstract int ID { get; }
    public bool ShouldExit = false;
    public MenuID NextMenuID;
    public abstract void Draw();
    public abstract void HandleInput(Window window);
}