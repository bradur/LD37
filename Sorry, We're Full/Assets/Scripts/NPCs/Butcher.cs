// Date   : 11.12.2016 10:00
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

public class Butcher : MonoBehaviour
{


    private bool intervalFinished = true;
    private float interval = 3f;
    private float timer;

    private KeyCode sellHide;
    private KeyCode sellMeat;

    void Start()
    {
        timer = interval;
        sellHide = KeyManager.main.GetKey(Action.SellHide);
        sellMeat = KeyManager.main.GetKey(Action.SellMeat);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (intervalFinished)
        {
            if (collision2D.gameObject.tag == "Player")
            {
                string message = "<color=brown><b>BUTCHER</b></color>: ";
                int countHides = InventoryManager.main.GetItemCount(InventoryItemType.Hide);
                int countMeat = InventoryManager.main.GetItemCount(InventoryItemType.Meat);
                if (countHides <= 0 && countMeat <= 0)
                {
                    message += string.Format(
                        "I will buy any <color=#{0}><b>{1}</b></color> or <color=#{2}><b>{3}</b></color> you have.",
                        UIManager.main.GetColorAsString(InventoryItemType.Meat),
                        InventoryItemType.Meat,
                        UIManager.main.GetColorAsString(InventoryItemType.Hide),
                        InventoryItemType.Hide
                    );
                    UIManager.main.ShowMessage(message);
                }
                else
                {
                    if (countHides > 0)
                    {
                        string hides = string.Format(
                            "{0}You have {1} <color=#{2}><b>{3}</b></color>{4}. Press {5} to sell. ",
                            message,
                            countHides,
                            UIManager.main.GetColorAsString(InventoryItemType.Hide),
                            InventoryItemType.Hide,
                            countHides > 1 ? "s" : "",
                            sellHide
                        );
                        UIManager.main.ShowMessage(hides);
                    }
                    if (countMeat > 0)
                    {
                        string meat = string.Format(
                            "{0}You have {1} <color=#{2}><b>{3}</b></color>. Press {4} to sell. ",
                            message,
                            countMeat,
                            UIManager.main.GetColorAsString(InventoryItemType.Meat),
                            InventoryItemType.Meat,
                            sellMeat
                        );
                        UIManager.main.ShowMessage(meat);
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
