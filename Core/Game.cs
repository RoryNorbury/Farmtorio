namespace Core;

public enum ItemID
{
    none=0,
    potato,
    carrot,
    barley,
    rice
}
public class Game
{
    private string _sourceDirectory = "..\\src";
    private Renderer _renderer;
    private Instance _instance = null;
    MenuID _currentMenu = MenuID.MainMenu;
    Menu[] _menus =
    [
        new MainMenu(),
        new SelectInstanceMenu(),
        new InstanceEscapeMenu()
    ];
    public Game()
    {
        
    }
    ~Game()
    {
        Close();
    }
    public void Close()
    {
        _instance.saveToFile(_sourceDirectory + "\\data\\saves");
        _renderer.Close();
    }
    public void Run()
    {
        bool shouldExit = false;
        while (!shouldExit)
        {
            if (_currentMenu == MenuID.Instance)
            {
                _instance.Tick(ref shouldExit);
            }
            else _menus[(int)_currentMenu].HandleInput(ref shouldExit);
        }
    }
}