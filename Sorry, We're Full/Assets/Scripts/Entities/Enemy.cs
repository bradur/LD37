// Date   : 11.12.2016 17:26
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private Customer parentCustomer;

    private RandomWrapper rng = new RandomWrapper();

    [SerializeField]
    private Transform target;

    [SerializeField]
    [Range(1, 25)]
    private int health = 5;
    public int Health { get { return health; } }

    [SerializeField]
    private bool isFighting = false;
    public bool IsFighting { set { isFighting = value; } }
    private bool isMoving = false;

    [SerializeField]
    private Rigidbody2D rb2D;
    [SerializeField]
    private EnemyWeapon enemyWeapon;
    public EnemyWeapon EnemyWeapon { get { return enemyWeapon; } }

    [SerializeField]
    [Range(1f, 6f)]
    private float initialWait = 3f;
    private float initialTimer;

    [SerializeField]
    [Range(0.5f, 3f)]
    private float moveInterval = 0.5f;
    private float moveTimer;

    [SerializeField]
    [Range(0.1f, 5f)]
    private float minDistance = 0.5f;

    [SerializeField]
    [Range(0.1f, 5f)]
    private float speed = 2f;

    [SerializeField]
    [Range(0.1f, 8f)]
    private float moveDrag = 2f;

    [SerializeField]
    [Range(0.1f, 3f)]
    private float hitInterval = 2f;
    private float hitTimer;

    public void GetHit(InventoryItemType weapon)
    {
        int healthLoss = 0;
        if (weapon == InventoryItemType.Dagger)
        {
            healthLoss = rng.Range(1, 3);
        }
        else if (weapon == InventoryItemType.ShortSword)
        {
            healthLoss = rng.Range(2, 5);
        }
        else if (weapon == InventoryItemType.Scimitar)
        {
            healthLoss = rng.Range(5, 8);
        }
        else if (weapon == InventoryItemType.Bow)
        {
            healthLoss = rng.Range(1, 3);
        }
        int newHealth = UIManager.main.AddToEnemyHealth(-healthLoss);
        if (newHealth < 1)
        {
            Die();
        }
        else
        {
            MoveAway();
        }

    }

    void MoveAway()
    {
        Vector2 direction = transform.position - target.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.position = position + direction * 0.15f;
        rb2D.AddForce(-direction * speed, ForceMode2D.Impulse);
    }

    void MoveTowards()
    {
        Vector2 direction = target.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.position = position + direction * 0.15f;
        rb2D.velocity = Vector2.zero;
        rb2D.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    void AttemptToHit()
    {
        Vector2 direction = transform.position - target.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.position = position + direction * 0.15f;
        enemyWeapon.SwingAt(target.position);
    }

    public void Init(Transform newTarget)
    {
        enemyWeapon.gameObject.SetActive(true);
        isFighting = true;
        WorldManager.main.CurrentFightingEnemy = this;
        UIManager.main.InitEnemyHealth(parentCustomer, this);
        target = newTarget;
        rb2D.drag = moveDrag;
        moveTimer = 0f;
        initialTimer = initialWait;
        gameObject.tag = "Enemy";
    }

    void Die()
    {
        WorldManager.main.CustomerWasBeaten(parentCustomer);
        Destroy(gameObject);
    }

    void Start()
    {
        if (isFighting)
        {// DEBUG CODE
            Init(WorldManager.main.Player);
            parentCustomer.IsFighting = true;
            GetComponent<RandomMovement>().enabled = false;
        }
    }

    void Update()
    {
        if (isFighting)
        {

            if (isMoving)
            {
                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0.01f)
                {
                    MoveTowards();
                    moveTimer = moveInterval;
                }
                hitTimer -= Time.deltaTime;

                if (hitTimer <= 0.01f)
                {
                    if (Vector2.Distance(transform.position, target.position) <= minDistance)
                    {
                        AttemptToHit();
                    }
                    hitTimer = hitInterval;
                }
            }
            else
            {
                initialTimer -= Time.deltaTime;
                if (initialTimer <= 0.01f)
                {
                    isMoving = true;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (isFighting)
        {
            if (collider2D.tag == "Projectile")
            {
                collider2D.transform.SetParent(transform, true);
                GetHit(InventoryItemType.Bow);
            }
        }
    }
}
