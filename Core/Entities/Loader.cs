namespace Core;

using SplashKitSDK;

public class Loader : Entity
{
    public override EntityID ID => EntityID.Loader;
    // speed at which items are moved (tiles per second)
    public double Speed = Globals.DefaultConveyorSpeed;
    // needs to be stored in order
    public List<ConveyorItem> Items = [];
    // entity in front of this one
    public Entity? NextEntity = null;
    public Entity? PreviousEntity = null;
    public Loader() : base() { }
    public Loader(double speed, List<ConveyorItem> items, Point2D position, OrientationID orientation, int textureIndex) : base(position, orientation, textureIndex)
    {
        Speed = speed;
        Items = items;
    }
    public Loader(double speed, List<ConveyorItem> items, Entity baseEntity) : this(speed, items, baseEntity.Position, baseEntity.Orientation, baseEntity.TextureIndex) { }
    public override List<string> GetSaveData()
    {
        List<string> data = base.GetSaveData();
        data.Add(Speed.ToString());
        return data;
    }
    public override void LoadFromData(List<string> data)
    {
        if (data.Count < 1)
        {
            throw new Exception($"Class 'Loader' requires at least 1 entry, {data.Count} provided.");
        }
        int i = data.Count - 1;
        Speed = double.Parse(data[i]);
        data.RemoveAt(i);
        base.LoadFromData(data);
    }
    // this function definitely needs to be split up and modularized
    public override void Tick(double dt)
    {
        // can't be ticked more than once per frame
        if (Ticked) { return; }
        Ticked = true;

        // if there isn't an entity in front and behind this, do not move any items
        // will eventually need to handle items when next or previous entity is deleted
        if (NextEntity == null || PreviousEntity == null)
        {
            return;
        }
        // can't load a loader
        if (NextEntity is Loader || PreviousEntity is Loader)
        {
            return;
        }
        // make sure next entity has already been processed (propogates forward)
        // same issues as in conveyor
        if (!NextEntity.Ticked)
        {
            NextEntity.Tick(dt);
        }
        if (!PreviousEntity.Ticked)
        {
            PreviousEntity.Tick(dt);
        }

        double dp = Speed * dt;
        // iterate in reverse order to ensure smooth flow
        for (int i = Items.Count - 1; i >= 0; i--)
        {
            ConveyorItem item = Items[i];

            // if this item is first
            if (i == Items.Count - 1)
            {
                // try to move item to next entity
                if (MoveItemToNextEntity(item, NextEntity, dp))
                {
                    Items.RemoveAt(i);
                }
            }
            // if this item is not first
            else
            {
                ConveyorItem nextItem = Items[i + 1];
                double dist = nextItem.Progress - item.Progress; // distance between current and next item

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
                    if (item.Progress >= 2.0)
                    {
                        throw new Exception($"Error moving item: item {i} of {Items.Count} has unexpectedly gone off the end of conveyor");
                    }
                }
            }
        }

        // attempt to grab an item from previous entity
        if (MoveItemFromPreviousEntity(PreviousEntity, dp))
        {
            // currently just do nothing
        }
    }
    // moves item and attempts to add it to next entity (returns true if successful)
    public bool MoveItemToNextEntity(ConveyorItem item, Entity nextEntity, double dp)
    {
        // if next entity has input slots
        if (nextEntity is IHasInputSlots hasInput)
        {
            if (item.Progress + dp >= 2.0)
            {
                foreach (InventorySlot slot in hasInput.InputSlots)
                {
                    if (slot.AddItems(1, item.ItemID)) { return true; }
                }
            }
            else
            {
                item.Progress = Math.Min(item.Progress + dp, 2.0);
                return false;
            }
        }
        // if next entity is a conveyor
        else if (nextEntity is Conveyor conveyor)
        {
            // easy check if conveyor only has one item
            if (conveyor.Items.Count > 0)
            {
                ConveyorItem nextItem = conveyor.Items[0];
                double dist = nextItem.Progress - item.Progress + 2; // distance between current and next item

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
                    if (item.Progress >= 2.0)
                    {
                        // move item to next conveyor
                        item.Progress -= 2.0;
                        conveyor.Items.Insert(0, item);
                        return true;
                    }
                }
                return false;
            }
            else
            {
                item.Progress += dp;
                if (item.Progress >= 2.0)
                {
                    // move item to next conveyor
                    item.Progress -= 2.0;
                    conveyor.Items.Insert(0, item);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }
    public bool MoveItemFromPreviousEntity(Entity previousEntity, double dp)
    {
        if (previousEntity is Conveyor conveyor)
        {
            // conveyor needs to face into the loader (subject to change)
            // if (conveyor.Orientation != Orientation)
            // {
            //     return false;
            // }
            // do nothing if there are no items to move
            if (conveyor.Items.Count == 0)
            {
                return false;
            }
            else
            // checks done, now grab item
            {
                // if there is space for the item -- shouldn't this be checked first??
                if (Items.Count == 0 || Items[0].Progress > Globals.ItemSize)
                {
                    // check each item to see if it is in grab range (only really affects conveyors not facing the loader)
                    for (int i = 0; i < conveyor.Items.Count; i++)
                    {
                        // only grab items if they are within one frame of movement of the pickup point (0.0)
                        if (conveyor.Items[i].Progress <= dp)
                        {
                            // currently items may not move the full distance when moving into a loader
                            Items.Insert(0, new ConveyorItem(conveyor.Items.Last().ItemID, 0));
                            conveyor.Items.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }
        else if (previousEntity is IHasOutputSlots hasOutput)
        {
            foreach (InventorySlot slot in hasOutput.OutputSlots)
            {
                if (slot.ItemCount > 0)
                {
                    // if there is space for the item
                    if (Items.Count == 0 || Items[0].Progress > Globals.ItemSize)
                    {
                        // currently items may not move the full distance when moving into a loader
                        Items.Insert(0, new ConveyorItem(slot.Item, 0));
                        slot.RemoveItems(1);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        return false;
    }
}