// Date   : 11.12.2016 14:12
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

public class Blacksmith : MonoBehaviour
{

    private bool intervalFinished = true;
    private float interval = 3f;
    private float timer;

    private KeyCode buyWeapon;
    private KeyCode buyArrows;

    private int iToldYou = 0;

    void Start()
    {
        timer = interval;
        buyWeapon = KeyManager.main.GetKey(Action.BuyWeapon);
        buyArrows = KeyManager.main.GetKey(Action.BuyArrows);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (intervalFinished)
        {
            if (collision2D.gameObject.tag == "Player")
            {
                InventoryItemType currentWeapon = WorldManager.main.Player.GetComponent<PlayerController>().EquippedWeapon;

                if (currentWeapon == InventoryItemType.Bow)
                {
                    currentWeapon = (InventoryItemType)(int)currentWeapon + 1;
                    if (iToldYou < 1)
                    {
                        UIManager.main.ShowMessage(string.Format(
                            "<color=yellow><b>BLACKSMITH</b></color>: I sell arrows and melee weapons.",
                            WorldManager.main.Business.RoomPrice,
                            UIManager.main.GetColorAsString(InventoryItemType.Coins),
                            InventoryItemType.Coins
                        ));
                        UIManager.main.ShowMessage("<color=yellow><b>BLACKSMITH</b></color>: To buy the best weapon I sell, you've got to purchase them all.");
                        UIManager.main.ShowMessage("<color=yellow><b>BLACKSMITH</b></color>: Don't worry. They're all the same price!");
                        iToldYou++;
                    }
                    else if (iToldYou >= 1)
                    {
                        UIManager.main.ShowMessage(string.Format(
                            "<color=yellow><b>BLACKSMITH</b></color>: <color=#{0}><b>{1}</b></color> {2} <color=#{3}><b>{4}</b></color> each. Press {5} to buy.",
                            UIManager.main.GetColorAsString(InventoryItemType.Arrows),
                            InventoryItemType.Arrows,
                            WorldManager.main.Business.ArrowPrice,
                            UIManager.main.GetColorAsString(InventoryItemType.Coins),
                            InventoryItemType.Coins,
                            KeyManager.main.GetKey(Action.BuyArrows)
                        ));
                        UIManager.main.ShowMessage(string.Format(
                            "<color=yellow><b>BLACKSMITH</b></color>: <color=#{0}><b>{1}</b></color> for {2} <color=#{3}><b>{4}</b></color>. Press {5} to buy.",
                            UIManager.main.GetColorAsString(currentWeapon),
                            currentWeapon,
                            WorldManager.main.Business.WeaponPrice,
                            UIManager.main.GetColorAsString(InventoryItemType.Coins),
                            InventoryItemType.Coins,
                            KeyManager.main.GetKey(Action.BuyWeapon)
                        ));
                        iToldYou++;
                    }
                }
                else
                {
                    if (currentWeapon != InventoryItemType.Scimitar)
                    {
                        currentWeapon = (InventoryItemType)(int)currentWeapon + 1;
                    }
                    UIManager.main.ShowMessage(string.Format(
                        "<color=yellow><b>BLACKSMITH</b></color>: <color=#{0}><b>{1}</b></color> {2} <color=#{3}><b>{4}</b></color> each. Press {5} to buy.",
                        UIManager.main.GetColorAsString(InventoryItemType.Arrows),
                        InventoryItemType.Arrows,
                        WorldManager.main.Business.ArrowPrice,
                        UIManager.main.GetColorAsString(InventoryItemType.Coins),
                        InventoryItemType.Coins,
                        KeyManager.main.GetKey(Action.BuyArrows)
                    ));
                    if (WorldManager.main.Player.GetComponent<PlayerController>().EquippedWeapon != InventoryItemType.Scimitar)
                    {
                        UIManager.main.ShowMessage(string.Format(
                            "<color=yellow><b>BLACKSMITH</b></color>: <color=#{0}><b>{1}</b></color> for {2} <color=#{3}><b>{4}</b></color>. Press {5} to buy.",
                            UIManager.main.GetColorAsString(currentWeapon),
                            currentWeapon,
                            WorldManager.main.Business.WeaponPrice,
                            UIManager.main.GetColorAsString(InventoryItemType.Coins),
                            InventoryItemType.Coins,
                            KeyManager.main.GetKey(Action.BuyWeapon)
                        ));
                    }
                }
                intervalFinished = false;
            }
        }
    }

    void Update()
    {
        if (!intervalFinished)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = interval;
                intervalFinished = true;
            }
        }
    }
}
