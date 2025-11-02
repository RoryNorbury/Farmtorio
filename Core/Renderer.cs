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
    public class Renderer
    {
    string[] textureFilenames =
    [
        "conveyor.png",
        "loader.png",
        "splitter.png",
        "manufactory.png",
        "farm.png",
        "depot.png",
    ];
    public Window window;
    private Bitmap[]? _textures;
    public Renderer(string textureFilename)
    {
        window = SplashKit.OpenWindow("Farmtorio", Globals.WindowWidth, Globals.WindowHeight);
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
        SplashKit.ClearScreen();
        DrawEntities(instance.DrawableEntities().ToArray());
        SplashKit.DrawInterface();
        SplashKit.RefreshScreen();
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