namespace Core;

using SplashKitSDK;

public abstract class ItemMover : Entity
{
    // speed at which items are moved (tiles per second)
    public abstract double Speed { get; set; }
    // needs to be stored in order
    public abstract List<ConveyorItem> Items { get; set; }
    public abstract double InputPosition { get; }
    public abstract double OutputPosition { get; }
    public ItemMover() : base() { }
    public ItemMover(Point2D position, OrientationID orientation, int textureIndex) : base(position, orientation, textureIndex) { }
    // if item is not at the front of the belt
    public void ProgressItem(ConveyorItem item, ConveyorItem nextItem, double dt)
    {
        double dp = Speed * dt;
        double dist = nextItem.Progress - item.Progress; // distance between current and next item

        // only move if there is space between items
        if (dist > Globals.ItemSize)
        {
            // if moving by dp will result in items that are too close, move them so the distance is equal to ItemSize
            if (dist - Globals.ItemSize < dp)
            {
                item.Progress += dp - (dist - Globals.ItemSize);
            }
            else
            {
                item.Progress += dp;
            }
        }
    }
    public void MoveItemFromConveyor(Conveyor previousEntity, double dt)
    {
        double dp = Speed * dt;
        if (previousEntity.Items.Count > 0)
        {
            if (Items.Count == 0 || Items[0].Progress > Globals.ItemSize)
            {
                // check each item to see if it is in grab range (only really affects conveyors not facing the loader)
                for (int i = 0; i < previousEntity.Items.Count; i++)
                {
                    // only grab items if they are within one frame of movement of the pickup point (0.0)
                    if (previousEntity.Items[i].Progress <= dp)
                    {
                        // currently items do not move when moving into a loader
                        Items.Insert(0, new ConveyorItem(previousEntity.Items[i].ItemID, 0));
                        previousEntity.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
    public void MoveItemToConveyor(int itemIndex, Conveyor nextConveyor, double dt)
    {
        double dp = Speed * dt;
        ConveyorItem item = Items[itemIndex];

        // if this item isn't first it can't move to next conveyor until the item in front has moved
        if (itemIndex != Items.Count - 1)
        {
            ConveyorItem nextItem = Items[itemIndex + 1];
            ProgressItem(item, nextItem, dp);
            if (item.Progress > OutputPosition)
            {
                throw new Exception($"Error moving item: item {itemIndex} of {Items.Count} has unexpectedly gone off the end of control range");
            }
        }
        // else item might be able to move to next conveyor
        else
        {
            // check if next conveyor has at least one item
            if (nextConveyor.Items.Count > 0)
            {
                ConveyorItem nextItem = nextConveyor.Items[0];
                //                                               size of this entity's control area
                double dist = nextItem.Progress - item.Progress + (OutputPosition - InputPosition); // distance between current and next item

                // only move if there is space between items
                if (dist > Globals.ItemSize)
                {
                    // if moving by dp will result in items that are too close, move them so the distance is equal to ItemSize
                    if (dist - dp < Globals.ItemSize)
                    {
                        item.Progress += dp - (dist - Globals.ItemSize);
                    }
                    else
                    {
                        item.Progress += dp;
                    }
                    if (item.Progress >= OutputPosition)
                    {
                        // move item to next conveyor
                        item.Progress -= (OutputPosition - InputPosition);
                        nextConveyor.Items.Insert(0, item);
                        Items.RemoveAt(itemIndex);
                    }
                }
            }
            else
            {
                item.Progress += dp;
                if (item.Progress >= OutputPosition)
                {
                    // move item to next conveyor
                    item.Progress -= (OutputPosition - InputPosition);
                    nextConveyor.Items.Insert(0, item);
                    Items.RemoveAt(itemIndex);
                }
            }
        }
    }
    public void MoveItemFromOutputSlot(IHasOutputSlots previousEntity, double dt)
    {
        double dp = Speed * dt;
        foreach (InventorySlot slot in previousEntity.OutputSlots)
        {
            if (slot.ItemCount > 0)
            {
                // if there is space for the item
                if (Items.Count == 0 || Items[0].Progress > Globals.ItemSize)
                {
                    // currently items do not move when moving into a loader
                    Items.Insert(0, new ConveyorItem(slot.Item, 0));
                    slot.RemoveItems(1);
                }
            }
        }
    }
    public void MoveItemToInputSlot(int itemIndex, IHasInputSlots nextEntity, double dt)
    {
        double dp = Speed * dt;
        ConveyorItem item = Items[itemIndex];
        // if this item isn't first it can't move to next entity until the item in front has moved
        if (itemIndex != Items.Count - 1)
        {
            ConveyorItem nextItem = Items[itemIndex + 1];
            ProgressItem(item, nextItem, dp);
            if (item.Progress > OutputPosition)
            {
                throw new Exception($"Error moving item: item {itemIndex} of {Items.Count} has unexpectedly gone off the end of control range");
            }
        }
        else
        {
            item.Progress = Math.Min(item.Progress + dp, (OutputPosition - InputPosition));
            if (item.Progress >= (OutputPosition - InputPosition))
            {
                foreach (InventorySlot slot in nextEntity.InputSlots)
                {
                    if (slot.AddItems(1, item.ItemID))
                    {
                        Items.Remove(item);
                        break;
                    }
                }
            }
        }
    }
}