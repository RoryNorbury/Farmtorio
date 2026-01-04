using SplashKitSDK;

namespace Core;

// TODO: save and load items on conveyors
public class Conveyor : Entity
{
    public override EntityID ID => EntityID.Conveyor;
    // speed at which items are moved (tiles per second)
    public double Speed = Globals.DefaultConveyorSpeed;
    // needs to be stored in order
    public List<ConveyorItem> Items = [];
    // entity in front of this one
    public Entity? NextEntity = null;
    // I'm too lazy to write all the constructors, this is all you get
    public Conveyor() : base() { }
    public Conveyor(double speed, List<ConveyorItem> items, Point2D position, OrientationID orientation, int textureIndex) : base(position, orientation, textureIndex)
    {
        Speed = speed;
        Items = items;
    }
    public Conveyor(double speed, List<ConveyorItem> items, Entity baseEntity) : this(speed, items, baseEntity.Position, baseEntity.Orientation, baseEntity.TextureIndex) { }
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
            throw new Exception($"Class 'Conveyor' requires at least 1 entry, {data.Count} provided.");
        }
        int i = data.Count - 1;
        Speed = double.Parse(data[i]);
        data.RemoveAt(i);
        base.LoadFromData(data);
    }
    public override void Tick(double dt)
    {
        // can't be ticked more than once per frame
        // having this at the start also avoids infinite nextEntity loops
        if (Ticked) { return; }
        // mark as ticked
        Ticked = true;

        // if belt is dead end, do not move any items (nowhere to move to, as items start at center of conveyor)
        // possibly change to have items move to end of conveyor (would only change behaviour if ItemSize is less than 0.5)
        if (NextEntity == null)
        {
            return;
        }
        // make sure next entity has already been processed (propogates forward)
        // this should be ok to do for any entity type
        if (!NextEntity.Ticked)
        {
            NextEntity.Tick(dt);
        }
        if (NextEntity is Conveyor nextConveyor)
        {
            double dp = Speed * dt;
            // iterate in reverse order to ensure smooth flow
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                ConveyorItem item = Items[i];

                // if this item is first
                if (i == Items.Count - 1)
                {
                    if (nextConveyor.Items.Count > 0)
                    {
                        ConveyorItem nextItem = nextConveyor.Items[0];
                        double dist = nextItem.Progress - item.Progress + 1; // distance between current and next item

                        // only move if there is space between items
                        if (dist > Globals.ItemSize)
                        {
                            if (dist - Globals.ItemSize < dp)
                            {
                                item.Progress += dp - (dist - Globals.ItemSize);
                            }
                            else
                            {
                                item.Progress += dp;
                            }
                            if (item.Progress >= 1.0)
                            {
                                // move item to next conveyor
                                item.Progress -= 1.0;
                                nextConveyor.Items.Insert(0, item);
                                Items.RemoveAt(i);
                            }
                        }
                    }
                    else
                    {
                        // move item
                        item.Progress += dp;
                        if (item.Progress >= 1.0)
                        {
                            // move item to next conveyor
                            item.Progress -= 1.0;
                            nextConveyor.Items.Insert(0, item);
                            Items.RemoveAt(i);
                        }
                    }
                }
                // if there is another item in front
                else
                {
                    ConveyorItem nextItem = Items[i + 1];
                    double dist = nextItem.Progress - item.Progress; // distance between current and next item
                    // if there is space between items
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
            }
        }
    }
}