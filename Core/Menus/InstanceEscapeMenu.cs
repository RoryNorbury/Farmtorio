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
        // BUG: Flickers the menu if escape key held down
        /*
            // will return to main menu if escape key pressed
            if (SplashKit.KeyDown(KeyCode.EscapeKey))
            {
                Game.NextMenuID = MenuID.Instance;
            }
        */

        Point windowSize = new Point(Globals.WindowWidth, Globals.WindowHeight);
        int numButtons = 5;
        int button = 0;
        if (SplashKit.Button("Return to Game", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding)))
        {
            Game.NextMenuID = MenuID.Instance;
        }
        button++;
        if (SplashKit.Button("Save Game", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding)))
        {
            Game.NextMenuID = MenuID.SaveInstanceMenu;
        }
        button++;
        if (SplashKit.Button("Load Game", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding)))
        {
            Game.NextMenuID = MenuID.SelectInstanceMenu;
            Game.GameInstance.UnloadInstance();
        }
        button++;
        if (SplashKit.Button("Return to menu", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding)))
        {
            Game.NextMenuID = MenuID.MainMenu;
            Game.GameInstance.UnloadInstance();
        }
        button++;
        if (SplashKit.Button("Quit", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding)))
        {
            ShouldExit = true;
            Game.GameInstance.UnloadInstance();
        }
    }
}