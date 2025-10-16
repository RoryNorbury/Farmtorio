namespace Core;

public abstract class Conveyor
{
    private ItemID[] _conveyorItems = new ItemID[8];
    public ItemID OutputItem = ItemID.none;
}