// Date   : 11.12.2016 17:42
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{

    private Vector3 cachedPosition;

    private bool isSwinging = false;
    private bool isBackSwinging = false;
    private Vector2 targetPosition;

    [SerializeField]
    [Range(2f, 20f)]
    private float speed = 4f;

    [SerializeField]
    [Range(1f, 10f)]
    private float backSpeed = 2f;

    [SerializeField]
    private InventoryItemType weapon;
    public InventoryItemType Weapon { get { return weapon; } }

    [SerializeField]
    private Customer parentCustomer;
    private Transform parent;

    void Start()
    {
        cachedPosition = transform.localPosition;
        parent = transform.parent;
    }

    public void SwingAt(Vector3 position)
    {
        if (!isSwinging && !isBackSwinging)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            Vector2 direction = transform.position - position;
            float angle = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            targetPosition = position;
            isSwinging = true;
        }
    }

    void Update()
    {
        if (isSwinging)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if(Vector2.Distance(transform.position, targetPosition) <= 0.2f)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                isSwinging = false;
                isBackSwinging = true;
            }
        }else if (isBackSwinging)
        {
            transform.position = Vector2.MoveTowards(transform.position, parent.TransformPoint(cachedPosition), backSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, parent.TransformPoint(cachedPosition)) <= 0.01f)
            {
                isBackSwinging = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.tag == "Player")
        {
            WorldManager.main.PlayerWasHit(parentCustomer, weapon);
        }
    }
}
