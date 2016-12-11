// Date   : 11.12.2016 10:46
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

public class Business : MonoBehaviour
{

    [SerializeField]
    private Transform blackSmith;

    [SerializeField]
    private Transform butcher;

    private KeyCode sellHide;
    private KeyCode sellMeat;

    private KeyCode buyWeapon;
    private KeyCode buyArrows;

    [SerializeField]
    [Range(0.2f, 5f)]
    private float minDistance = 1f;

    [SerializeField]
    [Range(1, 5)]
    private int hidePrice = 2;

    [SerializeField]
    [Range(1, 5)]
    private int meatPrice = 2;

    [SerializeField]
    [Range(1, 5)]
    private int arrowPrice = 2;
    void Start()
    {
        sellHide = KeyManager.main.GetKey(Action.SellHide);
        sellMeat = KeyManager.main.GetKey(Action.SellMeat);
        buyWeapon = KeyManager.main.GetKey(Action.BuyWeapon);
        buyArrows = KeyManager.main.GetKey(Action.BuyArrows);
    }

    void Update()
    {
        if (Input.GetKeyUp(sellHide))
        {
            Sell(InventoryItemType.Hide, hidePrice, butcher);
        }
        if (Input.GetKeyUp(sellMeat))
        {
            Sell(InventoryItemType.Meat, meatPrice, butcher);
        }
        if (Input.GetKeyUp(buyArrows))
        {
            Buy(InventoryItemType.Arrows, arrowPrice, blackSmith);
        }
    }

    void Buy(InventoryItemType type, int price, Transform vendor)
    {
        int coinCount = InventoryManager.main.GetItemCount(InventoryItemType.Coins);
        if (coinCount >= price)
        {
            InventoryManager.main.AddToCount(InventoryItemType.Coins, -price);
            InventoryManager.main.AddToCount(type, 1);
        }
        else
        {
            UIManager.main.ShowMessage(string.Format(
                "You don't have enough <color=#{0}><b>{1}</b></color> to buy <color=#{2}><b>{3}</b></color> ({4}).",
                UIManager.main.GetInventoryItemColor(InventoryItemType.Coins),
                InventoryItemType.Coins,
                UIManager.main.GetInventoryItemColor(type),
                type,
                price
            ));
        }
    }

    void Sell(InventoryItemType type, int price, Transform vendor)
    {

        int itemCount = InventoryManager.main.GetItemCount(type);
        if (Vector2.Distance(transform.position, vendor.transform.position) < minDistance)
        {
            if (itemCount > 0)
            {

                InventoryManager.main.AddToCount(type, -1);
                UIManager.main.ShowMessage(string.Format(
                    "You sold a <color=#{0}><b>{1}</b></color> for {2} <color=#{3}><b>{4}</b></color>.",
                    UIManager.main.GetInventoryItemColor(type),
                    type,
                    price,
                    UIManager.main.GetInventoryItemColor(InventoryItemType.Coins),
                    InventoryItemType.Coins
                ));
                InventoryManager.main.AddToCount(InventoryItemType.Coins, price);
            }
            else
            {
                UIManager.main.ShowMessage(string.Format(
                    "You don't have any <color=#{0}><b>{1}</b></color>{2}.",
                    UIManager.main.GetInventoryItemColor(type),
                    type,
                    type == InventoryItemType.Hide ? "s" : ""
                ));
            }
        }
    }
}
