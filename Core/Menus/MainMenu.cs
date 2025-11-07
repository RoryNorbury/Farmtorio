using SplashKitSDK;
namespace Core;
public class MainMenu : Menu
{
    public MainMenu() : base()
    {
        BackgroundColour = SplashKit.RGBColor(238, 238, 238);
    }
    public override MenuID ID
    {
        get { return MenuID.MainMenu; }
    }
    public override void Draw()
    {
        
    }
    public override void HandleInput()
    {
        Point windowSize = new Point(Globals.WindowWidth, Globals.WindowHeight);
        int numButtons = 3;
        int button = 0;
        if (SplashKit.Button("New Game", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding)))
        {
            Game.NewInstance();
            Game.GameInstance.NextMenuID = MenuID.Instance;
        }
        button++;
        if (SplashKit.Button("Load Game", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding)))
        {
            Game.GameInstance.NextMenuID = MenuID.SelectInstanceMenu;
        }
        button++;
        if (SplashKit.Button("Quit", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding)))
        {
            ShouldExit = true;
        }
    }
}