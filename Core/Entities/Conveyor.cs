namespace Core;

public abstract class Conveyor : Entity
{
    // effectively acts as padding between items (tiles)
    public static double ItemSize = 1.0;
    // speed at which items are moved (tiles per second)
    public double Speed = 0.5;
    // needs to be stored in order
    public List<ConveyorItem> Items = [];
    // entity in front of this one
    public Entity? NextEntity = null;
    public Conveyor(double speed)
    {
        Speed = speed;
    }
    public Conveyor(double speed, List<ConveyorItem> items)
    {
        Speed = speed;
        Items = items;
    }
    public override void Tick(double dt)
    {
        // mark as ticked
        Ticked = true;

        // if belt is dead end, do not move any items (nowhere to move to, as items start at center of conveyor)
        // possibly change to have items move to end of conveyor (would only change behaviour if ItemSize is less than 0.5)
        if (NextEntity == null)
        {
            return;
        }
        double dp = Speed * dt;
        // iterate in reverse order to ensure smooth flow
        for (int i = Items.Count - 1; i >= 0; i--)
        {
            ConveyorItem item = Items[i];
            // if there is another item in front
            if (i < Items.Count - 2)
            {
                ConveyorItem nextItem = Items[i + 1];
                double dist = nextItem.Progress - item.Progress; // distance between current and next item
                // if there is space between items
                if (dist > ItemSize)
                {
                    // if moving by dp will result in items that are too close, move them so the distance is equal to ItemSize
                    if (dist - ItemSize < dp)
                    {
                        item.Progress += dp - (dist - ItemSize);
                    }
                    else
                    {
                        item.Progress += dp;
                    }
                }
            }
            //possibly change so this check isn't done every tick
            else if (NextEntity is Conveyor)
            {
                Conveyor nextConveyor = NextEntity as Conveyor;
                if (nextConveyor.Items.Count > 0)
                {
                    ConveyorItem nextItem = nextConveyor.Items[0];
                    double dist = nextItem.Progress - item.Progress + 1; // distance between current and next item
                    if (dist > ItemSize)
                    {
                        if (dist - ItemSize < dp)
                        {
                            item.Progress += dp - (dist - ItemSize);
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