using SplashKitSDK;

namespace Core;

public static class Globals
{
    // Paths
    public static string SourceDirectory = "..\\src\\";
    public static string TexturesDirectory = SourceDirectory + "\\Textures\\";
    public static string SavesDirectory = SourceDirectory + "\\data\\saves\\";

    // BASIS FOR ALL OTHER SIZE VALUES
    public static int WindowWidth = 1280;

    public static int WindowHeight = WindowWidth * 9 / 16;

    // GUI element constants
    public static int StandardElementWidth = WindowWidth * 3 / 16;
    public static int StandardElementHeight = WindowWidth * 3 / 64;
    public static int StandardElementPadding = WindowWidth * 3 / 128;
    public static int StandardElementBorder = WindowWidth / 640;

    public static int SmallElementWidth = StandardElementWidth;
    public static int SmallElementHeight = StandardElementHeight / 2;
    public static int SmallElementPadding = StandardElementPadding / 2;

    // Rendering
    public static int fps = 60;
    // represents movement speed of camera in world units per frame
    public static double CameraSpeed = 4.0 / fps;
    // represents amount of pixels per world unit
    public static double ZoomScale = 64.0f;
    public static Color BackgroundColor = SplashKit.RGBColor(238, 238, 238);
    public static Color GridColor = SplashKit.RGBColor(200, 200, 200);
}