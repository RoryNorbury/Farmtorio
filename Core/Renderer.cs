using SplashKitSDK;
namespace Core;


// TODO: make my own camera class, that allows zooming
public class Renderer
{
    public Window window;
    private List<Bitmap> _textures = new List<Bitmap>();
    public Renderer()
    {
        window = SplashKit.OpenWindow("Farmtorio", Globals.WindowWidth, Globals.WindowHeight);
        LoadTexturesFromFile();
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
        SplashKit.RefreshScreen((uint)Globals.fps);
    }
    public void RenderInstance(Instance instance)
    {
        SplashKit.ClearScreen(Globals.BackgroundColor);
        DrawGrid();
        DrawEntities(instance.DrawableEntities().ToArray());
        SplashKit.DrawInterface();
        SplashKit.RefreshScreen((uint)Globals.fps);
    }
    public void DrawEntities(Entity[] entities)
    {
        foreach (Entity entity in entities)
        {
            DrawEntity(entity);
        }
    }
    public void DrawEntity(Entity entity)
    {
        Bitmap texture = _textures[(int)entity.ID];
        DrawingOptions options = new DrawingOptions();
        if (!entity.isDirectional)
        {
            options = SplashKit.OptionRotateBmp(0);
        }
        else
        {
            switch (entity.Orientation)
            {
                case OrientationID.East:
                    options = SplashKit.OptionRotateBmp(0);
                    break;
                case OrientationID.South:
                    options = SplashKit.OptionRotateBmp(90);
                    break;
                case OrientationID.West:
                    options = SplashKit.OptionRotateBmp(180);
                    break;
                case OrientationID.North:
                    options = SplashKit.OptionRotateBmp(270);
                    break;
            }
        }
        Point2D drawPos = Instance.WorldToScreenCoords(entity.Position);
        options.Camera = DrawingDest.DrawToScreen;
        SplashKit.DrawBitmap(texture, drawPos.X, drawPos.Y, options);
    }

    private void LoadTexturesFromFile()
    {
        try
        {
            _textures = new List<Bitmap>();
            for (int i = 0; i < Entity.EntityIDStrings.Length; i++)
            {
                _textures.Add(SplashKit.LoadBitmap(Entity.EntityIDStrings[i], Entity.EntityIDStrings[i] + ".png"));
            }
        }
        catch (Exception e)
        {
            throw new Exception("Error loading textures: " + e.Message);
        }
    }
    private void DrawGrid()
    {
        // tell splashkit not to apply camera transformations
        DrawingOptions options = SplashKit.OptionToScreen();

        // the offset from the tile grid, in screen coordinates
        Point2D offset = Instance.WorldToScreenCoords(new Point2D() { X = Math.Floor(Camera.X), Y = Math.Floor(Camera.Y) });
        // move offset to the top-left corner, while staying aligned to the grid
        offset.X -= Globals.WindowWidth / (2 * Globals.ZoomScale) * Globals.ZoomScale;
        offset.Y -= Globals.WindowWidth / (2 * Globals.ZoomScale) * Globals.ZoomScale;
        // draw lines until the end of the screen
        for (double x = offset.X; x < Globals.WindowWidth; x += Globals.ZoomScale)
        {
            SplashKit.DrawLine(Globals.GridColor, x, 0, x, Globals.WindowHeight, options);
        }
        for (double y = offset.Y; y < Globals.WindowHeight; y += Globals.ZoomScale)
        {
            SplashKit.DrawLine(Globals.GridColor, 0, y, Globals.WindowWidth, y, options);
        }
    }
}