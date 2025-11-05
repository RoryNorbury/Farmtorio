namespace Core;

public class InventorySlot
{
    private ItemID _item = ItemID.none;
    public ItemID Item => _item;
    private int _quantity = 0;
    public int Quantity => _quantity;
    public InventorySlot() { }
    public InventorySlot(ItemID item, int quantity)
    {
        _item = item;
        _quantity = quantity;
    }
    public bool AddItems(int quantity, ItemID itemID)
    {
        if (quantity <= 0) { throw new Exception($"You cant add {quantity} items, you silly goose"); }
        if (_item == ItemID.none)
        {
            _item = itemID;
            _quantity += quantity;
            return true;
        }
        else if (_item == itemID)
        {
            _quantity += quantity;
            return true;
        }
        return false;
    }
    public bool RemoveItems(int quantity)
    {
        if (quantity <= 0) { throw new Exception($"You cant remove {quantity} items, you silly goose"); } // this should probably just return false
        if (_quantity - quantity < 0)
        {
            return false;
        }
        _quantity -= quantity;
        if (_quantity == 0)
        {
            _item = ItemID.none;
        }
        return true;
    }
}