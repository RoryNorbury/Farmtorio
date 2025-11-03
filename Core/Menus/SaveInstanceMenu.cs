using SplashKitSDK;
namespace Core;
public class SaveInstanceMenu : Menu
{
    public SaveInstanceMenu() : base()
    {
        BackgroundColour = SplashKit.RGBColor(238, 238, 238);
    }
    private string userEntry = "";
    public override int ID
    {
        get { return (int)MenuID.SaveInstanceMenu; }
    }
    public override void Draw()
    {
        
    }
    public override void HandleInput()
    {
        Point windowSize = new Point(Globals.WindowWidth, Globals.WindowHeight);
        int numButtons = 3;
        int button = 0;
        if (SplashKit.Button("Go back", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding)))
        {
            Game.NextMenuID = MenuID.InstanceEscapeMenu;
        }

        // text entry area
        Rectangle rect = Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding);
        rect.Y += Globals.StandardElementHeight + Globals.StandardElementPadding + (Globals.StandardElementHeight - 2 * Globals.SmallElementHeight) / 2;
        rect.Height = Globals.SmallElementHeight * 2;

        SplashKit.StartInset("SaveNameInput", rect);
        SplashKit.LabelElement("Enter save name:");
        userEntry = SplashKit.TextBox("Save name", userEntry);
        SplashKit.EndInset("SaveNameInput");
        if (userEntry == "") { SplashKit.DisableInterface(); }

        button+=2;
        if (SplashKit.Button("Save Game", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding)))
        {
            Game.GameInstance.SaveInstance(Globals.SavesDirectory + userEntry);
            Game.NextMenuID = MenuID.Instance;
        }
        SplashKit.EnableInterface();
    }
}