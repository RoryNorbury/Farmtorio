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
    public override void HandleInput(Window window)
    {
        // should find a way to have this happen automatically - put in game?
        // should find a way to have this happen automatically - put in game?
        NextMenuID = (MenuID)ID;

        Point windowSize = new Point(window.Width, window.Height);
        int numButtons = 3;
        int buttonWidth = 240;
        int buttonHeight = 60;
        int buttonPadding = 30;
        int button = 0;
        if (SplashKit.Button("Hi :)", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, buttonWidth, buttonHeight, buttonPadding)))
        {
            Console.WriteLine("Hello!");
        }
        button++;
        if (SplashKit.Button("SelectInstance", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, buttonWidth, buttonHeight, buttonPadding)))
        {
            NextMenuID = MenuID.SelectInstanceMenu;
            Console.WriteLine("Ok!");
        }
        button++;
        if (SplashKit.Button("Quit", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, buttonWidth, buttonHeight, buttonPadding)))
        {
            Console.WriteLine("Bye!");
            ShouldExit = true;
        }
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
        
    }
    public override void HandleInput(Window window)
    {
        NextMenuID = (MenuID)ID;

        // will return to main menu if escape key pressed
        if (SplashKit.KeyDown(KeyCode.EscapeKey))
        {
            NextMenuID = MenuID.MainMenu;
        }

        Point windowSize = new Point(window.Width, window.Height);
        int numButtons = 2;
        int buttonWidth = 240;
        int buttonHeight = 60;
        int buttonPadding = 30;
        int button = 0;
        if (SplashKit.Button("Hi :)", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, buttonWidth, buttonHeight, buttonPadding)))
        {
            Console.WriteLine("Hello!");
        }
        button++;
        if (SplashKit.Button("Go back", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, buttonWidth, buttonHeight, buttonPadding)))
        {
            NextMenuID = MenuID.MainMenu;
        }
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
    }
    public override void HandleInput(Window window)
    {
        NextMenuID = (MenuID) ID;
    }
}