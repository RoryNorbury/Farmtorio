using SplashKitSDK;
namespace Core;

public class SelectInstanceMenu : Menu
{
    public SelectInstanceMenu() : base()
    {
        BackgroundColour = SplashKit.RGBColor(238, 238, 238);
    }
    public override int ID
    {
        get { return (int)MenuID.SelectInstanceMenu; }
    }
    public override void Draw()
    {

    }
    public override void HandleInput()
    {
        Game.NextMenuID = (MenuID)ID; // probably not needed anymore

        // will return to main menu if escape key pressed
        if (SplashKit.KeyDown(KeyCode.EscapeKey))
        {
            Game.NextMenuID = MenuID.MainMenu;
        }

        // rename to element*Trait* ?
        int numButtons = 2;
        int numInstanceEntries = 4;
        Rectangle elementRect =
        new Rectangle
        {
            X = (Globals.WindowWidth - Globals.StandardElementWidth) / 2,
            Y = (Globals.WindowHeight - (Globals.StandardElementHeight * numButtons + Globals.SmallElementHeight * numInstanceEntries + Globals.StandardElementPadding * numButtons + Globals.StandardElementBorder * (numInstanceEntries + 1))) / 2,
            Width = Globals.StandardElementWidth,
            Height = Globals.StandardElementHeight
        };
        if (SplashKit.Button("Hi :)", elementRect))
        {
            Console.WriteLine("Hello!");
        }
        elementRect.Y += Globals.StandardElementHeight + Globals.StandardElementPadding;
        if (SplashKit.Button("Go back", elementRect))
        {
            Game.NextMenuID = MenuID.MainMenu;
        }
        elementRect.Y += Globals.StandardElementHeight + Globals.StandardElementPadding;
        elementRect.Height = Globals.SmallElementHeight * numInstanceEntries + Globals.StandardElementBorder * (numInstanceEntries + 1);
        List<string> instanceNames = new List<string>(Directory.GetFiles(Globals.SavesDirectory, "*.txt"));

        // display available instances
        SplashKit.StartInset("SelectInstance", elementRect);
        for (int i = 0; i < instanceNames.Count; i++)
        {
            if (SplashKit.Button(instanceNames[i].Split('\\').Last().Replace(".txt", "")))
            {
                try
                {
                    Game.LoadInstance(instanceNames[i]);
                    Game.NextMenuID = MenuID.Instance;
                    Console.WriteLine("Loaded instance: " + instanceNames[i]);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error loading instance: " + e.Message);
                    Game.NextMenuID = MenuID.SelectInstanceMenu;
                }
            }
        }
        SplashKit.EndInset("SelectInstance");
    }
}