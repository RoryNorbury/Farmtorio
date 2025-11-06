namespace Core;
using SplashKitSDK;

public class ConveyorItem
{
    public ItemID ItemID = 0;
    // distance along the conveyor in tiles
    public double Progress = 0;
    public ConveyorItem(ItemID id, float progress)
    {
        ItemID = id;
        Progress = progress;
    }
    public static Color[] ItemColours = [
        SplashKit.RGBColor(0, 0, 0),
        SplashKit.RGBColor(220, 200, 130),
        SplashKit.RGBColor(255, 170, 60),
        SplashKit.RGBColor(250, 235, 150),
        SplashKit.RGBColor(235, 230, 215)
    ];
}