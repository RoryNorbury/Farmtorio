using SplashKitSDK;

namespace Core;

public class MainMenu : Menu
{
    public MainMenu()
    {
        BackgroundColour = SplashKit.RGBColor(238, 238, 238);
        NextMenuID = (MenuID) ID;
    }
    public override int ID
    {
        get { return (int)MenuID.MainMenu; }
    }
    public override void Draw()
    {
        
    }
    public override void HandleInput()
    {
        
    }
}
public class SelectInstanceMenu : Menu
{
    public SelectInstanceMenu()
    {
        BackgroundColour = SplashKit.RGBColor(238, 238, 238);
        NextMenuID = (MenuID) ID;
    }
    public override int ID
    {
        get { return (int)MenuID.SelectInstanceMenu; }
    }
    public override void Draw()
    {
        throw new NotImplementedException();
    }
    public override void HandleInput()
    {
        throw new NotImplementedException();
    }
}
public class InstanceEscapeMenu : Menu
{
    public InstanceEscapeMenu()
    {
        BackgroundColour = SplashKit.RGBAColor(238, 238, 238, 64);
        NextMenuID = (MenuID) ID;
    }
    public override int ID
    {
        get { return (int)MenuID.InstanceEscapeMenu; }
    }
    public override void Draw()
    {
        throw new NotImplementedException();
    }
    public override void HandleInput()
    {
        throw new NotImplementedException();
    }
}