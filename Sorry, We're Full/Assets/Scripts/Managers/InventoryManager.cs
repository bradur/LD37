// Date   : 10.12.2016 16:40
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager main;

    [SerializeField]
    private UIInventoryItem uiInventoryItemPrefab;

    [SerializeField]
    private List<InventoryItem> ownedItems = new List<InventoryItem>();

    [SerializeField]
    private List<UIInventoryItem> uiOwnedItems = new List<UIInventoryItem>();

    [SerializeField]
    private Transform inventoryContainer;

    void Awake()
    {
        main = this;
    }

    public void Loot(List<InventoryItem> lootItems)
    {
        foreach (InventoryItem lootItem in lootItems)
        {
            AddToInventory(lootItem);
        }
    }

    void Start()
    {
        for (int i = 0; i < ownedItems.Count; i++)
        {
            Debug.Log("Adding " + ownedItems[i].InventoryItemType);
            AddToUI(ownedItems[i], i);
        }
    }

    public void RemoveFromInventory(InventoryItem inventoryItem)
    {
        ownedItems.Remove(inventoryItem);
        uiOwnedItems.Remove(FindUIOwnedItem(inventoryItem));
    }

    UIInventoryItem FindUIOwnedItem(InventoryItem inventoryItem)
    {
        foreach (UIInventoryItem uiOwnedItem in uiOwnedItems)
        {
            if (uiOwnedItem.InventoryItem.InventoryItemType == inventoryItem.InventoryItemType)
            {
                return uiOwnedItem;
            }
        }
        return null;
    }

    void AddToUI(InventoryItem lootItem, int index = -1)
    {
        Vector2 position;
        if (index == -1)
        {
            position = new Vector2(0f, -62.5f - ownedItems.Count * uiInventoryItemPrefab.GetComponent<RectTransform>().sizeDelta.y);
        } else
        {
            position = new Vector2(0f, -62.5f - index * uiInventoryItemPrefab.GetComponent<RectTransform>().sizeDelta.y);
        }
        UIInventoryItem item = Instantiate(uiInventoryItemPrefab);
        item.Init(lootItem, position, inventoryContainer);
        uiOwnedItems.Add(item);
    }

    void AddToInventory(InventoryItem lootItem)
    {
        bool isNewItem = false;
        InventoryItem tempItem;
        foreach (InventoryItem ownedItem in ownedItems)
        {
            if (lootItem.InventoryItemType == ownedItem.InventoryItemType)
            {
                isNewItem = false;
                ownedItem.Count += lootItem.Count;
                FindUIOwnedItem(ownedItem).UpdateItemCount(ownedItem.Count);
                break;
            }
            else
            {
                isNewItem = true;
                tempItem = lootItem;
            }
        }
        if (isNewItem)
        {
            AddToUI(lootItem);
            ownedItems.Add(lootItem);
        }
    }
}
