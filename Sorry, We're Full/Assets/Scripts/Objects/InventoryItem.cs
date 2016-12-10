// Date   : 10.12.2016 16:36
// Project: Sorry, We're Full
// Author : bradur


public enum InventoryItemType
{
    None,
    Hide,
    Meat,
    Coins
}

[System.Serializable]
public class InventoryItem : System.Object {
    public InventoryItemType InventoryItemType;
    public int Count;
    public UnityEngine.Sprite Sprite;
}
