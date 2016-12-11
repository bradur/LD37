// Date   : 10.12.2016 09:00
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
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
    private KeyCode hitKey;

    [SerializeField]
    private InventoryItemType currentWeapon = InventoryItemType.Bow;
    public InventoryItemType CurrentWeapon { get { return currentWeapon; } }

    private InventoryItemType equippedWeapon = InventoryItemType.Bow;
    public InventoryItemType EquippedWeapon { get { return equippedWeapon; }  set { equippedWeapon = value; } }

    Vector3 direction;
    Vector3 arrowDirection;

    void Start()
    {
        shootKey = KeyManager.main.GetKey(Action.Shoot);
        hitKey = KeyManager.main.GetKey(Action.Hit);
        if (rb2D == null)
        {
            rb2D = GetComponent<Rigidbody2D>();
        }
        direction = -transform.right;
        arrowDirection = direction;
        projectileContainer = WorldManager.main.ProjectileContainer;
    }

    void Update()
    {
        if (currentWeapon == InventoryItemType.Bow)
        {
            if (Input.GetKeyUp(shootKey))
            {
                Shoot();
            }
        }
        else if (equippedWeapon != InventoryItemType.Bow)
        {
            if (Input.GetKeyUp(hitKey))
            {
                SwingWeapon();
            }
        }
    }

    void Shoot()
    {
        if (InventoryManager.main.GetItemCount(InventoryItemType.Arrows) > 0)
        {
            SoundManager.main.PlaySound(SoundType.ProjectileLaunch);
            Projectile projectile = Instantiate(projectilePrefab);
            projectile.Launch(transform.position, transform.position + arrowDirection, projectileContainer);
            InventoryManager.main.AddToCount(InventoryItemType.Arrows, -1);
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

        if (Mathf.Abs(horizontalAxis) >= 0.05f || Mathf.Abs(verticalAxis) >= 0.05f)
        {
            arrowDirection = direction;
        }
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
            InventoryManager.main.AddToInventory(InventoryItemType.Arrows, 1);
            Destroy(collider2D.gameObject);
        }
        else if (collider2D.tag == "Animal")
        {
            InventoryManager.main.Loot(collider2D.GetComponent<Animal>().Loot());
        }

    }
}
