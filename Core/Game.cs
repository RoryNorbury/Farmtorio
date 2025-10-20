namespace Core;

using System.Diagnostics;
using SplashKitSDK;

public enum ItemID
{
    none = 0,
    potato,
    carrot,
    barley,
    rice
}
public class Game
{
    private string _sourceDirectory = "..\\src";
    private Renderer _renderer;
    private Instance? _instance = null;
    MenuID _currentMenu = MenuID.MainMenu;
    Menu[] _menus =
    [
        new MainMenu(),
        new SelectInstanceMenu(),
        new InstanceEscapeMenu()
    ];
    public Game()
    {
        _renderer = new Renderer(_sourceDirectory + "\\Textures");
    }
    ~Game()
    {
        Close();
    }
    public void Close()
    {
        if (_instance != null) { _instance.saveToFile(_sourceDirectory + "\\data\\saves"); }
        _renderer.Close();
    }
    public void Run()
    {
        try
        {
            bool shouldExit = false;
            while (!shouldExit)
            {
                SplashKit.ProcessEvents();
                if (_currentMenu == MenuID.Instance)
                {
                    if (_instance == null) { throw new Exception("_instance object is null, but you are trying to tick it"); }
                    _instance.Tick();
                    _renderer.RenderInstance(_instance);
                    if (_instance.ShouldExit) { shouldExit = true; }
                    ;
                }
                else
                {
                    Menu menu = _menus[(int)_currentMenu];
                    menu.HandleInput(_renderer.window);
                    _renderer.RenderMenu(menu);
                    _currentMenu = menu.NextMenuID;
                    if (menu.ShouldExit) { shouldExit = true; }
                }
                if (SplashKit.QuitRequested()) { shouldExit = true; }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error thrown: {e.GetType()}: {e.Message}\nStack trace: {e.StackTrace}");
        }
    }
}