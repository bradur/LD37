// Date   : 11.12.2016 08:27
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField]
    private Material houseMaterial;
    [SerializeField]
    private Material outsideMaterial;

    [SerializeField]
    private bool goingIn = true;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        int value = 0;
        if (goingIn)
        {
            value = 1;
        }
        if (collider2D.tag == "Player")
        {
            
            houseMaterial.SetFloat("_Transparency", value);
            outsideMaterial.SetFloat("_Darken", value);
            WorldManager.main.Player.GetComponent<Business>().PlayerIsInside(goingIn);
        }
    }

    void Start () {
    
    }

    void Update () {
    
    }
}
