// Date   : 11.12.2016 09:12
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

public class InnKeeperTable : MonoBehaviour {

    void Start () {
    
    }

    void Update () {
    
    }

    [SerializeField]
    private InnKeep innKeep;

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.tag == "Player")
        {
            UIManager.main.ShowMessage("<color=green><b>INNKEEPER</b></color>: We only have one room. And it's taken!");
        }
    }
}
