// Date   : 10.12.2016 09:53
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

public class WorldManager : MonoBehaviour
{

    public static WorldManager main;

    [SerializeField]
    private Material outMaterial;
    [SerializeField]
    private Material houseMaterial;

    [SerializeField]
    private Transform projectileContainer;
    public Transform ProjectileContainer { get { return projectileContainer; } }

    [SerializeField]
    private Transform player;
    public Transform Player { get { return player; } }

    void Awake()
    {
        main = this;
        outMaterial.SetFloat("_Darken", 0);
        houseMaterial.SetFloat("_Transparency", 0);
    }

}
