namespace Core;

using SplashKitSDK;

public class Loader : ItemMover
{
    public override EntityID ID => EntityID.Loader;
    // speed at which items are moved (tiles per second)
    public override double Speed { get; set; } = Globals.DefaultConveyorSpeed;
    // needs to be stored in order
    public override List<ConveyorItem> Items { get; set; } = [];
    // entity in front of this one
    public Entity? NextEntity = null;
    public Entity? PreviousEntity = null;

    public override double InputPosition => -1.0;
    public override double OutputPosition => 1.0;
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

        // iterate in reverse order to ensure smooth flow
        for (int i = Items.Count - 1; i >= 0; i--)
        {
            MoveItemToNextEntity(i, NextEntity, dt);
        }

        // attempt to grab an item from previous entity
        MoveItemFromPreviousEntity(PreviousEntity, dt);
    }
}