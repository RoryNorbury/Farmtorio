namespace Core;

using SplashKitSDK;

public class Farm : Entity, IHasOutputSlots
{
    public override EntityID ID => EntityID.Farm;
    public override bool isDirectional => false;
    public ItemID CropType = ItemID.none;
    public int GrowthTime = 0; // in ticks
    public List<InventorySlot> OutputSlots { get; } = [new InventorySlot()];
    public Farm() : base()
    {
        // temporary random crop type assignment
        Random rand = new Random();
        CropType = (ItemID)rand.Next(1, Enum.GetValues<ItemID>().Length);
    }
    public Farm(ItemID cropType, List<InventorySlot> outputSlots, Point2D position, int textureIndex) : base(position, OrientationID.North, textureIndex)
    {
        CropType = cropType;
        OutputSlots = outputSlots;
    }
    public Farm(ItemID cropType, List<InventorySlot> outputSlots, Entity baseEntity) : this(cropType, outputSlots, baseEntity.Position, baseEntity.TextureIndex) { }
    public override List<string> GetSaveData()
    {
        List<string> data = base.GetSaveData();
        data.Add(((int)CropType).ToString());
        return data;
    }
    public override void LoadFromData(List<string> data)
    {
        if (data.Count < 1)
        {
            throw new Exception($"Class 'Farm' requires at least 1 entry, {data.Count} provided.");
        }
        int i = data.Count - 1;
        CropType = (ItemID)int.Parse(data[i]);
        data.RemoveAt(i);
        base.LoadFromData(data);
    }
    public override void Tick(double dt)
    {
        Ticked = true;
        GrowthTime++;
        if (GrowthTime >= Globals.CropHarvestTime)
        {
            // try to add crop to output slots
            bool added = OutputSlots[0].AddItems(1, CropType);
            if (added)
            {
                GrowthTime = 0;
            }
        }
    }

}