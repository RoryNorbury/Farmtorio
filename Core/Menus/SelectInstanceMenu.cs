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