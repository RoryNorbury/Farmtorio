using SplashKitSDK;
namespace Core;

public class Renderer
{
    private Bitmap[] _textures;
    public Renderer(string textureFilename)
    {
        LoadTexturesFromFile(textureFilename);
    }
    public void Close()
    {
        SplashKit.CloseAllWindows();
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