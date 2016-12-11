// Date   : 10.12.2016 16:36
// Project: Sorry, We're Full
// Author : bradur


public enum InventoryItemType
{
    None,
    Hide,
    Meat,
    Coins,
    Arrows,
    Bow,
    Dagger,
    ShortSword,
    Scimitar
}

[System.Serializable]
public class InventoryItem : System.Object {

    public InventoryItem(InventoryItemType type, int count, UnityEngine.Sprite sprite)
    {
        this.InventoryItemType = type;
        this.Count = count;
        this.Sprite = sprite;
    }

    public InventoryItem(InventoryItemType type, int count)
    {
        this.InventoryItemType = type;
        this.Count = count;
    }

    public InventoryItemType InventoryItemType;
    public int Count;
    public UnityEngine.Sprite Sprite;
}
