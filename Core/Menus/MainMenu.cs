using SplashKitSDK;
namespace Core;
public class MainMenu : Menu
{
    public MainMenu() : base()
    {
        BackgroundColour = SplashKit.RGBColor(238, 238, 238);
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


        Point windowSize = new Point(Globals.WindowWidth, Globals.WindowHeight);
        int numButtons = 3;
        int button = 0;
        if (SplashKit.Button("Hi :)", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding)))
        {
            Console.WriteLine("Hello!");
        }
        button++;
        if (SplashKit.Button("SelectInstance", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding)))
        {
            Game.NextMenuID = MenuID.SelectInstanceMenu;
            Console.WriteLine("Ok!");
        }
        button++;
        if (SplashKit.Button("Quit", Helpers.getMenuButtonRectangle(windowSize, numButtons, button, Globals.StandardElementWidth, Globals.StandardElementHeight, Globals.StandardElementPadding)))
        {
            Console.WriteLine("Bye!");
            ShouldExit = true;
        }
    }
}