using Core;

public class ConveyorItem
{
    public ItemID itemID = 0;
    // distance along the conveyor (1 is one tile)
    public double Progress = 0;
    public ConveyorItem(ItemID id, float progress)
    {
        itemID = id;
        Progress = progress;
    }
}