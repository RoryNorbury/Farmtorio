namespace Core;

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
}