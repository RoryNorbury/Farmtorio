namespace Core;
using SplashKitSDK;

public class Depot : Entity, IHasInputSlots
{
    public override EntityID ID => EntityID.Depot;
    public override bool isDirectional => false;
    public int SellCount = 0;
    public List<InventorySlot> InputSlots { get; } = [new InventorySlot()];
    public Depot() : base() { }
    public Depot(int sellCount, List<InventorySlot> inputSlots, Point2D position, int textureIndex) : base(position, OrientationID.North, textureIndex)
    {
        SellCount = sellCount;
        InputSlots = inputSlots;
    }
    public Depot(int sellCount, List<InventorySlot> inputSlots, Entity baseEntity) : this(sellCount, inputSlots, baseEntity.Position, baseEntity.TextureIndex) { }
    public override List<string> GetSaveData()
    {
        List<string> data = base.GetSaveData();
        data.Add(SellCount.ToString());
        return data;
    }
    public override void LoadFromData(List<string> data)
    {
        if (data.Count < 1)
        {
            throw new Exception($"Class 'Depot' requires at least 1 entry, {data.Count} provided.");
        }
        int i = data.Count - 1;
        SellCount = int.Parse(data[i]);
        data.RemoveAt(i);
        base.LoadFromData(data);
    }
    public override void Tick(double dt)
    {
        // can't be ticked more than once per frame
        if (Ticked) { return; }
        Ticked = true;

        // sell all items in input slots (currently only one)
        foreach (InventorySlot slot in InputSlots)
        {
            SellCount += slot.ItemCount;
            slot.Clear();
        }
    }
}