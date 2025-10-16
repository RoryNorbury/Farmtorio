namespace Core;

public enum MenuID
{
    MainMenu,
    SelectInstanceMenu,
    InstanceEscapeMenu,
    Instance
}
public class MainMenu : Menu
{
    public override int ID
    {
        get { return (int)MenuID.MainMenu; }
    }
    public override void Draw()
    {
        throw new NotImplementedException();
    }
    public override void HandleInput()
    {
        throw new NotImplementedException();
    }
}
public class SelectInstanceMenu : Menu
{
    public override int ID
    {
        get { return (int)MenuID.SelectInstanceMenu; }
    }
    public override void Draw()
    {
        throw new NotImplementedException();
    }
    public override void HandleInput()
    {
        throw new NotImplementedException();
    }
}
public class InstanceEscapeMenu : Menu
{
    public override int ID
    {
        get { return (int)MenuID.InstanceEscapeMenu; }
    }
    public override void Draw()
    {
        throw new NotImplementedException();
    }
    public override void HandleInput()
    {
        throw new NotImplementedException();
    }
}