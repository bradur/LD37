// Date   : 10.12.2016 09:57
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using System.Collections.Generic;

public enum Action
{
    None,
    Shoot,
    Hit,
    Buy,
    Exit,
    Confirm,
    Cancel
}

public class KeyManager : MonoBehaviour {

    public static KeyManager main;

    private void Awake()
    {
        main = this;
    }

    [SerializeField]
    private List<GameKey> gameKeys = new List<GameKey>();

    public KeyCode GetKey(Action action)
    {
        foreach(GameKey gameKey in gameKeys)
        {
            if (gameKey.action == action)
            {
                return gameKey.key;
            }
        }
        return KeyCode.None;
    }
}


[System.Serializable]
public class GameKey : System.Object
{
    public KeyCode key;
    public Action action;
}