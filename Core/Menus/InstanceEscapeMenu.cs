using SplashKitSDK;
namespace Core;
public class InstanceEscapeMenu : Menu
{
    public InstanceEscapeMenu() : base()
    {
        BackgroundColour = SplashKit.RGBAColor(238, 238, 238, 64);
    }
    public override int ID
    {
        get { return (int)MenuID.InstanceEscapeMenu; }
    }
    public override void Draw()
    {
    }
    public override void HandleInput()
    {
    }
}