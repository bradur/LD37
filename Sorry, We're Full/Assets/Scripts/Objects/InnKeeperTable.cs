// Date   : 11.12.2016 09:12
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

public class InnKeeperTable : MonoBehaviour
{

    private bool intervalFinished = true;
    private float interval = 3f;
    private float timer;

    [SerializeField]
    private InnKeep innKeep;

    private int iToldYou = 0;
    public int IToldYou { set { iToldYou = value; } }


    void Start()
    {
        timer = interval;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (intervalFinished)
        {
            if (collision2D.gameObject.tag == "Player")
            {
                if (!WorldManager.main.CustomerHasBeenBeat)
                {
                    if (iToldYou == 0)
                    {
                        UIManager.main.ShowMessage(string.Format(
                            "<color=green><b>INNKEEPER</b></color>: We only have one room, {0} <color=#{1}><b>{2}</b></color> for a night.",
                            WorldManager.main.Player.GetComponent<Business>().RoomPrice,
                            UIManager.main.GetColorAsString(InventoryItemType.Coins),
                            InventoryItemType.Coins
                        ));
                        UIManager.main.ShowMessage(string.Format(
                            "<color=green><b>INNKEEPER</b></color>: But it's taken!"
                        ));
                        iToldYou++;
                    }
                    else if (iToldYou == 1)
                    {
                        UIManager.main.ShowMessage("<color=green><b>INNKEEPER</b></color>: You again?");
                        UIManager.main.ShowMessage(string.Format(
                            "<color=green><b>INNKEEPER</b></color>: I already told you, the room is {0} <color=#{1}><b>{2}</b></color> for a night.",
                            WorldManager.main.Player.GetComponent<Business>().RoomPrice,
                            UIManager.main.GetColorAsString(InventoryItemType.Coins),
                            InventoryItemType.Coins
                        ));
                        UIManager.main.ShowMessage("<color=green><b>INNKEEPER</b></color>: But it's TAKEN. Understand?");
                        UIManager.main.ShowMessage("<color=green><b>INNKEEPER</b></color>: Pah! Go talk to the other customer and see if you can make a deal.");
                        iToldYou++;
                    }
                    else if (iToldYou == 2)
                    {
                        UIManager.main.ShowMessage("<color=green><b>INNKEEPER</b></color>: Did you make a deal?");
                        iToldYou++;
                    }
                    else if (iToldYou <= 5)
                    {
                        UIManager.main.ShowMessage("<color=green><b>INNKEEPER</b></color>: Hello.");
                        iToldYou++;
                    }
                    else
                    {
                        UIManager.main.ShowMessage("<color=green><b>INNKEEPER</b></color>: ...");
                    }
                    intervalFinished = false;
                }
                else
                {
                    if (iToldYou == 0)
                    {
                        UIManager.main.ShowMessage(string.Format(
                            "<color=green><b>INNKEEPER</b></color>: We only have one room, {0} <color=#{1}><b>{2}</b></color> for a night.",
                            WorldManager.main.Player.GetComponent<Business>().RoomPrice,
                            UIManager.main.GetColorAsString(InventoryItemType.Coins),
                            InventoryItemType.Coins
                        ));
                        UIManager.main.ShowMessage(string.Format(
                            "<color=green><b>INNKEEPER</b></color>: And you're in luck, sir!"
                        ));
                        UIManager.main.ShowMessage("<color=green><b>INNKEEPER</b></color>: The room has just been made available!");
                        UIManager.main.ShowMessage(string.Format(
                            "Press {0} to rent the room and spend the night.",
                            KeyManager.main.GetKey(Action.RentRoom)
                        ));
                        iToldYou++;
                    }
                    else if (iToldYou == 1)
                    {
                        UIManager.main.ShowMessage("<color=green><b>INNKEEPER</b></color>: Hello! Here to rent the room?");
                        iToldYou++;
                    }
                    else if (iToldYou <= 5)
                    {
                        UIManager.main.ShowMessage("<color=green><b>INNKEEPER</b></color>: Hello.");
                        iToldYou++;
                    }
                    else
                    {
                        UIManager.main.ShowMessage("<color=green><b>INNKEEPER</b></color>: Rent the room or scram!");
                    }
                    intervalFinished = false;
                }
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
