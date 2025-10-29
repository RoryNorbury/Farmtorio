using SplashKitSDK;
namespace Core;

// possible figure out a way to have these auto assigned
enum TextureID
{
    Conveyor,
    Loader,
    Splitter,
    Manufactory,
    Farm,
    Depot,
}
string[] mtextureFilenames = [
    "conveyor.png",
    "loader.png",
    "splitter.png",
    "manufactory.png",
    "farm.png",
    "depot.png",
];
public class Renderer
{
    public Window window;
    private Bitmap[]? _textures;
    public Point Resolution = new Point(720, 360);
    public Renderer(string textureFilename)
    {
        window = SplashKit.OpenWindow("Farmtorio", (int)Resolution.X, (int)Resolution.Y);
        LoadTexturesFromFile(textureFilename);
    }
    public void Close()
    {
        SplashKit.CloseAllWindows();
    }
    public void RenderMenu(Menu menu)
    {
        SplashKit.ClearScreen(menu.BackgroundColour);
        menu.Draw();
        SplashKit.DrawInterface();
        SplashKit.RefreshScreen();
    }
    public void RenderInstance(Instance instance)
    {
        DrawEntities(instance.DrawableEntities());
    }
    public void DrawEntities(Entity[] entities)
    {

    }
    public void DrawEntity(Entity entity)
    {

    }
    private void LoadTexturesFromFile(string filename)
    {
        _textures = new Bitmap[0];
    }
}