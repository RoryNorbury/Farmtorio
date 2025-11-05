namespace Core;

// TODO: implement stack size limits
// TODO: implement saving/loading
public class InventorySlot
{
    private ItemID _item = ItemID.none;
    public ItemID Item => _item;
    private int _itemCount = 0;
    public int ItemCount => _itemCount;
    public InventorySlot() { }
    public InventorySlot(ItemID item, int quantity)
    {
        _item = item;
        _itemCount = quantity;
    }
    public bool AddItems(int quantity, ItemID itemID)
    {
        if (quantity <= 0) { throw new Exception($"You cant add {quantity} items, you silly goose"); }
        if (_item == ItemID.none)
        {
            _item = itemID;
            _itemCount += quantity;
            return true;
        }
        else if (_item == itemID)
        {
            _itemCount += quantity;
            return true;
        }
        return false;
    }
    public bool RemoveItems(int quantity)
    {
        if (quantity <= 0) { throw new Exception($"You cant remove {quantity} items, you silly goose"); } // this should probably just return false
        if (_itemCount - quantity < 0)
        {
            return false;
        }
        _itemCount -= quantity;
        if (_itemCount == 0)
        {
            _item = ItemID.none;
        }
        return true;
    }
    public void Clear()
    {
        _item = ItemID.none;
        _itemCount = 0;
    }
}