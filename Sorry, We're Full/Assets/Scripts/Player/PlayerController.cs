// Date   : 10.12.2016 09:00
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    [Range(2f, 10f)]
    private float speedForward = 2f;

    [SerializeField]
    private Projectile projectilePrefab;
    private Rigidbody2D rb2D;
    private Transform projectileContainer;

    private KeyCode shootKey;

    Vector3 direction;

    void Start()
    {
        shootKey = KeyManager.main.GetKey(Action.Shoot);
        if (rb2D == null)
        {
            rb2D = GetComponent<Rigidbody2D>();
        }
        direction = -transform.right;
        projectileContainer = WorldManager.main.ProjectileContainer;
    }

    void Update()
    {
        if (Input.GetKeyUp(shootKey))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Projectile projectile = Instantiate(projectilePrefab);
        projectile.Launch(transform.position, transform.position + direction, projectileContainer);
        Debug.Log("Attempting to shoot!");
    }

    void FixedUpdate()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        direction = new Vector3(horizontalAxis * speedForward, verticalAxis * speedForward, 0f);
        rb2D.velocity = direction;

    }
    
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
