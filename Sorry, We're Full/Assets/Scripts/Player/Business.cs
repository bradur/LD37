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

    [SerializeField]
    private Transform innKeeper;

    private KeyCode sellHide;
    private KeyCode sellMeat;

    private KeyCode buyWeapon;
    private KeyCode rentRoom;
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

    [SerializeField]
    [Range(5, 10)]
    private int roomPrice = 3;
    public int RoomPrice { get { return roomPrice; } }

    private bool inside = false;

    void Start()
    {
        sellHide = KeyManager.main.GetKey(Action.SellHide);
        sellMeat = KeyManager.main.GetKey(Action.SellMeat);
        rentRoom = KeyManager.main.GetKey(Action.RentRoom);
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
        if (Input.GetKeyUp(rentRoom))
        {
            BuyRoom();
        }
    }

    public void PlayerIsInside(bool isInside)
    {
        inside = isInside;
    }

    void BuyRoom()
    {
        if (inside)
        {
            int coinCount = InventoryManager.main.GetItemCount(InventoryItemType.Coins);
            if (coinCount >= roomPrice)
            {
                if (WorldManager.main.CustomerHasBeenBeat)
                {
                    InventoryManager.main.AddToCount(InventoryItemType.Coins, -roomPrice);
                    UIManager.main.ShowMessage(string.Format(
                        "You rented the room for the night for {0} <color=#{1}><b>{2}</b></color>.",
                        roomPrice,
                        UIManager.main.GetColorAsString(InventoryItemType.Coins),
                        InventoryItemType.Coins
                    ));
                }
                else
                {
                    UIManager.main.ShowMessage(string.Format(
                        "<color=green><b>INNKEEPER</b></color>: You can't rent the room, it's still taken!"
                    ));
                }
            }
            else
            {
                UIManager.main.ShowMessage(string.Format(
                    "<color=green><b>INNKEEPER</b></color>: The room's {0} <color=#{1}><b>{2}</b></color> for a night. {3}",
                    roomPrice,
                    UIManager.main.GetColorAsString(InventoryItemType.Coins),
                    InventoryItemType.Coins,
                    coinCount > 0
                        ? string.Format("You only have {0} <color=#{1}><b>{2}</b></color>!", coinCount, UIManager.main.GetColorAsString(InventoryItemType.Coins), InventoryItemType.Coins)
                        : string.Format("You don't have any <color=#{0}><b>{1}</b></color>!", UIManager.main.GetColorAsString(InventoryItemType.Coins), InventoryItemType.Coins)
                ));
            }
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
                UIManager.main.GetColorAsString(InventoryItemType.Coins),
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
                    UIManager.main.GetColorAsString(InventoryItemType.Coins),
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
