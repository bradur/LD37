// Date   : 10.12.2016 09:53
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

public class WorldManager : MonoBehaviour
{

    public static WorldManager main;

    [SerializeField]
    private Transform projectileContainer;
    public Transform ProjectileContainer { get { return projectileContainer; } }

    void Awake()
    {
        main = this;
    }


}
