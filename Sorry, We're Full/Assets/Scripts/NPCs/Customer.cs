using UnityEngine;
// Project: Sorry, We're Full
// Author : bradur

// Date   : 11.12.2016 09:19 UnityEngine;

public class Customer : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Player")
        {
            UIManager.main.ShowMessage("<color=green><b>CUSTOMER</b></color>: Give you my room? Hah! You're going to have to fight me for it.");
        }
    }
}
