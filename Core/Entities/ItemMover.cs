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
    protected void MoveItemToNextEntity(int itemIndex, Entity nextEntity, double dt)
    {
        if (nextEntity is Conveyor nextConveyor)
        {
            MoveItemToConveyor(itemIndex, nextConveyor, dt);
        }
        else if (nextEntity is IHasInputSlots nextHasInputSlots)
        {
            MoveItemToInputSlot(itemIndex, nextHasInputSlots, dt);
        }
        else
        {
            throw new Exception($"Error moving item: entity in front of ItemMover is of type {nextEntity.GetType().Name}, which is not a valid type to move to");
        }
    }
    protected void MoveItemFromPreviousEntity(Entity previousEntity, double dt)
    {
        if (previousEntity is Conveyor previousConveyor)
        {
            MoveItemFromConveyor(previousConveyor, dt);
        }
        else if (previousEntity is IHasOutputSlots previousHasOutputSlots)
        {
            MoveItemFromOutputSlot(previousHasOutputSlots, dt);
        }
        else
        {
            throw new Exception($"Error moving item: entity behind ItemMover is of type {previousEntity.GetType().Name}, which is not a valid type to move from");
        }
    }
    protected void MoveItemFromConveyor(Conveyor previousConveyor, double dt)
    {
        double dp = Speed * dt;
        if (previousConveyor.Items.Count > 0)
        {
            if (Items.Count == 0 || (Items[0].Progress - InputPosition) > Globals.ItemSize)
            {
                // check each item to see if it is in grab range (only really affects conveyors not facing the loader)
                for (int i = 0; i < previousConveyor.Items.Count; i++)
                {
                    // only grab items if they are within one frame of movement of the pickup point (0.0)
                    if (previousConveyor.Items[i].Progress <= dp)
                    {
                        // currently items do not move when moving into a loader
                        Items.Insert(0, new ConveyorItem(previousConveyor.Items[i].ItemID, InputPosition));
                        previousConveyor.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
    protected void MoveItemToConveyor(int itemIndex, Conveyor nextConveyor, double dt)
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
                // distance between current and next item
                double dist = nextItem.Progress - nextConveyor.InputPosition + OutputPosition - item.Progress;

                // only move if there is space between items
                if (dist > Globals.ItemSize)
                {
                    // if moving by dp will result in items that are too close, move them so the distance is equal to ItemSize
                    if (dist - dp < Globals.ItemSize)
                    {
                        item.Progress += dp - (dist - Globals.ItemSize);
                    }
                    else { item.Progress += dp; }
                    if (item.Progress >= OutputPosition)
                    {
                        // move item to next conveyor
                        item.Progress = item.Progress - OutputPosition + nextConveyor.InputPosition;
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
                    item.Progress = item.Progress - OutputPosition + nextConveyor.InputPosition;
                    nextConveyor.Items.Insert(0, item);
                    Items.RemoveAt(itemIndex);
                }
            }
        }
    }
    protected void MoveItemFromOutputSlot(IHasOutputSlots previousHasOutput, double dt)
    {
        double dp = Speed * dt;
        foreach (InventorySlot slot in previousHasOutput.OutputSlots)
        {
            if (slot.ItemCount > 0)
            {
                // if there is space for the item
                if (Items.Count == 0 || (Items[0].Progress - InputPosition) > Globals.ItemSize)
                {
                    // currently items do not move when moving into a loader
                    Items.Insert(0, new ConveyorItem(slot.Item, InputPosition));
                    slot.RemoveItems(1);
                }
            }
        }
    }
    protected void MoveItemToInputSlot(int itemIndex, IHasInputSlots nextHasInput, double dt)
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
            item.Progress = Math.Min(item.Progress + dp, OutputPosition);
            if (item.Progress >= OutputPosition)
            {
                foreach (InventorySlot slot in nextHasInput.InputSlots)
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