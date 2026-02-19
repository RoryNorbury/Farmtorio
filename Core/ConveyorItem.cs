namespace Core;

using System.Reflection.Metadata.Ecma335;
using SplashKitSDK;

public class ConveyorItem
{
    public ItemID ItemID = 0;
    // distance along the conveyor in tiles
    public double Progress = 0;
    public ConveyorItem(ItemID id, double progress)
    {
        ItemID = id;
        Progress = progress;
    }
    private static Color[] ItemColours = [
        SplashKit.RGBColor(0, 0, 0),
        SplashKit.RGBColor(220, 200, 130),
        SplashKit.RGBColor(255, 170, 60),
        SplashKit.RGBColor(250, 235, 150),
        SplashKit.RGBColor(235, 230, 215)
    ];
    public static Color GetItemColour(ItemID id)
    {
        return ItemColours[(int)id];
    }
    public static string GetItemName(ItemID id)
    {
        return id switch
        {
            ItemID.none => "none",
            ItemID.potato => "Potato",
            ItemID.carrot => "Carrot",
            ItemID.barley => "Barley",
            ItemID.rice => "Rice",
            _ => throw new InvalidDataException($"ItemId '{id}' is invalid")
        };
    }
}