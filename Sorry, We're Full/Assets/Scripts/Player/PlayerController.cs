// Date   : 10.12.2016 09:00
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

public enum Weapon
{
    None,
    Bow,
    Dagger
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    [Range(2f, 10f)]
    private float speedForward = 2f;

    private int projectileCount = 5;

    [SerializeField]
    private Projectile projectilePrefab;
    private Rigidbody2D rb2D;
    private Transform projectileContainer;

    private KeyCode shootKey;
    private KeyCode hitKey;

    [SerializeField]
    private Weapon currentWeapon = Weapon.None;

    Vector3 direction;

    void Start()
    {
        shootKey = KeyManager.main.GetKey(Action.Shoot);
        hitKey = KeyManager.main.GetKey(Action.Hit);
        if (rb2D == null)
        {
            rb2D = GetComponent<Rigidbody2D>();
        }
        direction = -transform.right;
        projectileContainer = WorldManager.main.ProjectileContainer;
    }

    void Update()
    {
        if (currentWeapon == Weapon.Bow)
        {
            if (Input.GetKeyUp(shootKey))
            {
                Shoot();
            }
        } else if (currentWeapon != Weapon.None)
        {
            if (Input.GetKeyUp(hitKey))
            {
                SwingWeapon();
            }
        }
    }

    void Shoot()
    {
        if (projectileCount > 0)
        {
            SoundManager.main.PlaySound(SoundType.ProjectileLaunch);
            Projectile projectile = Instantiate(projectilePrefab);
            projectile.Launch(transform.position, transform.position + direction, projectileContainer);
            projectileCount -= 1;
        }
        else
        {
            SoundManager.main.PlaySound(SoundType.OutOfAmmo);
            UIManager.main.ShowMessage(currentWeapon, MessageType.NoAmmo);
        }
    }

    void SwingWeapon()
    {
        SoundManager.main.PlaySound(SoundType.SwingWeapon);
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

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Projectile")
        {
            SoundManager.main.PlaySound(SoundType.PickUpProjectile);
            projectileCount += 1;
            Destroy(collider2D.gameObject);
        }
    }
}
