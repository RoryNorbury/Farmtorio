using SplashKitSDK;
namespace Core;

public class SaveInstanceMenu : Menu
{
    public SaveInstanceMenu() : base()
    {
        BackgroundColour = SplashKit.RGBColor(238, 238, 238);
    }
    private string userEntry = null;
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
            userEntry = null;
        }

        // text entry area
        Rectangle rect = Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding);
        rect.Y += Globals.StandardElementHeight + Globals.StandardElementPadding + (Globals.StandardElementHeight - 2 * Globals.SmallElementHeight) / 2;
        rect.Height = Globals.SmallElementHeight * 2;

        // should only occur once when menu is opened
        if (userEntry == null)
        { userEntry = Game.GameInstance.InstanceName; }

        SplashKit.StartInset("SaveNameInput", rect);
        SplashKit.LabelElement("Enter save name:");
        userEntry = SplashKit.TextBox("Save name", userEntry);
        // ensure filename is valid
        userEntry = userEntry.Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "");
        SplashKit.EndInset("SaveNameInput");
        if (userEntry == "") { SplashKit.DisableInterface(); }

        button += 2;
        if (SplashKit.Button("Save Game", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding)))
        {
            try
            {
                Game.GameInstance.SaveInstance(Globals.SavesDirectory + userEntry + ".txt");
                Game.GameInstance.InstanceName = userEntry;
                Game.NextMenuID = MenuID.Instance;
                userEntry = null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error saving instance: " + e.Message);
            }
        }
        SplashKit.EnableInterface();
    }
}