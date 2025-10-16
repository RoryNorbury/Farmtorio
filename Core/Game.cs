namespace Core;

public enum ItemID
{
    potato,
    carrot,
    barley,
    rice
}
public class Game
{
    private Renderer _renderer;
    private Instance _instance;
    private MenuID _currentMenu;
    private Menu[] _menus;
    public Game()
    {
        Initialise();
    }
    ~Game()
    {
        Close();
    }
    public void Initialise()
    {
        throw new NotImplementedException();
    }
    public void Close()
    {
        throw new NotImplementedException();
    }
    public void Run()
    {
        throw new NotImplementedException();
    }
}