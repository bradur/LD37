// Date   : 10.12.2016 16:45
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class UIInventoryItem : MonoBehaviour {

    [SerializeField]
    private Text txtItemTitle;
    [SerializeField]
    private Text txtItemCount;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgItem;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private InventoryItem inventoryItem;

    public InventoryItem InventoryItem { get { return inventoryItem; } }

    public void Init(InventoryItem item, Vector2 position, Transform parent)
    {
        transform.SetParent(parent, false);
        GetComponent<RectTransform>().anchoredPosition = position;
        inventoryItem = item;
        txtItemCount.text = item.Count.ToString();
        txtItemTitle.text = item.InventoryItemType.ToString();
        imgItem.sprite = item.Sprite;
    }

    public void UpdateItemCount(int count)
    {
        if(count == 0)
        {
            RemoveItem();
        }
        txtItemCount.text = count.ToString();
        UIManager.main.ShowMessage(MessageType.ItemCountUpdate, inventoryItem.InventoryItemType, count);
    }

    public void RemoveItem()
    {
        if(inventoryItem.InventoryItemType != InventoryItemType.Coins)
        {
            InventoryManager.main.RemoveFromInventory(inventoryItem);
            animator.SetTrigger("Remove");
        }
    }

}
