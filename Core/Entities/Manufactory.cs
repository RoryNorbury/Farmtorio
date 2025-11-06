namespace Core;

using SplashKitSDK;

// TODO: Add recipe handling
public class Manufactory : Entity, IHasInputSlots, IHasOutputSlots
{
    public override EntityID ID => EntityID.Manufactory;
    public override bool isDirectional => false;
    public int RecipeTime = 0; // in ticks
    public List<InventorySlot> InputSlots { get; } = [new InventorySlot(), new InventorySlot()];
    public List<InventorySlot> OutputSlots { get; } = [new InventorySlot(), new InventorySlot()];
    public Manufactory() : base() { }
    public Manufactory(Point2D position, int textureIndex) : base(position, OrientationID.North, textureIndex) { }
    public Manufactory(Entity baseEntity) : this(baseEntity.Position, baseEntity.TextureIndex) { }
    public override List<string> GetSaveData()
    {
        List<string> data = base.GetSaveData();
        return data;
    }
    public override void LoadFromData(List<string> data)
    {
        base.LoadFromData(data);
    }
    public override void Tick(double dt)
    {
        // can't be ticked more than once per frame
        if (Ticked) { return; }
        Ticked = true;

        if (InputSlots[0].Item != ItemID.none || InputSlots[1].Item != ItemID.none)
        {
            RecipeTime++;
            if (RecipeTime >= Globals.RecipeTime)
            {
                // process recipe
                // currently just converts input items to output items of same type
                for (int i = 0; i < InputSlots.Count; i++)
                {
                    ItemID inputItem = InputSlots[i].Item;
                    int inputCount = InputSlots[i].ItemCount;
                    if (inputItem != ItemID.none && inputCount > 0)
                    {
                        // try to add to output slots
                        bool added = false;
                        for (int j = 0; j < OutputSlots.Count; j++)
                        {
                            if (OutputSlots[j].AddItems(inputCount, inputItem))
                            {
                                added = true;
                                break;
                            }
                        }
                        if (added)
                        {
                            InputSlots[i].RemoveItems(inputCount);
                        }
                    }
                }
                RecipeTime = 0;
            }
        }
        else
        {
            RecipeTime = 0;
        }
    }
}