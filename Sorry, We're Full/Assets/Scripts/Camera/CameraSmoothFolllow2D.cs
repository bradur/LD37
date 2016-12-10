// Date   : 10.12.2016 11:41
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

public class CameraSmoothFolllow2D : MonoBehaviour {

    private Transform target;

    [SerializeField]
    [Range(0.2f, 2f)]
    private float dampenTime = 0.5f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 newPos;

    void Start () {
        target = WorldManager.main.Player;
    }

    void LateUpdate () {
        if (target)
        {
            newPos = Vector3.SmoothDamp(transform.position, target.position, ref velocity, dampenTime);
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
    }
}
