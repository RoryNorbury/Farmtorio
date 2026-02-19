using SplashKitSDK;

namespace Core;

// TODO: save and load items on conveyors
public class Conveyor : ItemMover
{
    public override EntityID ID => EntityID.Conveyor;
    // speed at which items are moved (tiles per second)
    public override double Speed { get; set; } = Globals.DefaultConveyorSpeed;
    // needs to be stored in order
    public override List<ConveyorItem> Items { get; set; } = [];
    // entity in front of this one
    public Entity? NextEntity = null;
    public override double InputPosition => 0.0;
    public override double OutputPosition => 1.0;
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
            // iterate in reverse order to ensure smooth flow
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                MoveItemToConveyor(i, nextConveyor, dt);
            }
        }
    }
}