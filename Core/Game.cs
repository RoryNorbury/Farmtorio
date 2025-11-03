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
// make singleton
public sealed class Game
{
    // singleton stuff
    private static Game? _game = null;
    public static Game GameInstance
    {
        get
        {
            if (_game == null)
            {
                _game = new Game();
            }
            return _game;
        }
    }

    private static Renderer _renderer;
    private static Instance? _instance = null;
    private static MenuID _currentMenu = MenuID.MainMenu;
    public static MenuID NextMenuID = MenuID.MainMenu; // can this be done just using currentmenu?
    private static Menu[] _menus =
    [
        new MainMenu(),
        new SelectInstanceMenu(),
        new InstanceEscapeMenu(),
        new SaveInstanceMenu()
    ];
    private Game()
    {
        _renderer = new Renderer(Globals.TexturesDirectory);
    }
    ~Game()
    {
        Close();
    }
    public void Close()
    {
        if (_instance != null) { _instance.SaveToFile(Globals.SavesDirectory + _instance.Name + ".txt"); }
        _renderer.Close();
    }
    public void Run()
    {
        try
        {
            bool shouldExit = false;
            double dt = 1.0 / 60.0;
            while (!shouldExit)
            {
                SplashKit.ProcessEvents();
                if (_currentMenu == MenuID.Instance)
                {
                    if (_instance == null) { throw new Exception("_instance object is null, but you are trying to tick it"); }
                    _instance.Tick(dt);
                    _renderer.RenderInstance(_instance);
                    _currentMenu = NextMenuID;
                    if (_instance.ShouldExit) { shouldExit = true; }
                }
                else
                {
                    Menu menu = _menus[(int)_currentMenu];
                    menu.HandleInput();
                    _renderer.RenderMenu(menu);
                    _currentMenu = NextMenuID;
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
    public static void NewInstance()
    {
        _instance = new Instance();
    }
    public static void LoadInstance(string filepath)
    {
        try
        {
            if (_instance == null)
            {
                _instance = new Instance(filepath);
            }
            else
            {
                _instance.LoadFromFile(filepath);
            }
        }
        catch (Exception e)
        {
            throw new Exception("Could not load instance: " + e.Message);
        }
    }
    public void UnloadInstance()
    {
        _instance = null;
    }
    public void SaveInstance(string filepath)
    {
        if (_instance == null)
        {
            throw new Exception("No instance loaded to save");
        }
        _instance.SaveToFile(filepath);
    }
}