// Date   : 10.12.2016 12:26
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager main;

    void Awake()
    {
        main = this;
    }

    public void ShowMessage(string message)
    {
        Debug.Log(message);
    }
}
