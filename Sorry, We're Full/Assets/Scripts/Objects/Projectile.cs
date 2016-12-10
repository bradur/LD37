// Date   : 10.12.2016 09:20
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{

    private Rigidbody2D rb2D;

    [SerializeField]
    [Range(1f, 50f)]
    private float lifeTime = 25f;
    private float currentLifeTime;

    [SerializeField]
    [Range(1f, 50f)]
    private float speed = 1f;

    private float stoppingspeed = 0.2f;

    [SerializeField]
    private bool useLifeTime = false;
    private bool launched = false;

    private bool pickable = false;
    private bool isHit = false;
    private LayerMask launchedLayer;
    private LayerMask pickableLayer;

    private CapsuleCollider2D capsuleCollider2D;

    public void Launch(Vector2 position, Vector2 direction, Transform parent)
    {
        if (!capsuleCollider2D)
        {
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }
        launchedLayer = gameObject.layer;
        pickableLayer = LayerMask.NameToLayer("Pickable Projectile");
        transform.SetParent(parent, false);
        direction = direction - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = position + direction * 0.15f;

        currentLifeTime = lifeTime;
        if (!launched)
        {
            if (rb2D == null)
            {
                rb2D = GetComponent<Rigidbody2D>();
            }
            rb2D.AddForce(transform.right * speed, ForceMode2D.Impulse);
            launched = true;
        }
    }

    public void PickUp()
    {
        /* if you pool this, do this:
        if (pickable)
        {
            
            
            pickable = false;
            launched = false;

            capsuleCollider2D.isTrigger = false;
            gameObject.SetActive(false);
            gameObject.layer = launchedLayer;
            
        }*/
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Animal")
        {
            isHit = true;
            rb2D.velocity = Vector2.zero;
            rb2D.isKinematic = true;
        }
    }

    void Update()
    {
        if (!isHit) { 
            if (useLifeTime)
            {
                currentLifeTime -= Time.deltaTime;
                if (currentLifeTime < 0)
                {
                    Destroy(gameObject);
                }
            }
            if (launched && rb2D.velocity.magnitude <= stoppingspeed)
            {
                launched = false;
                pickable = true;
                gameObject.layer = pickableLayer;
                rb2D.velocity = Vector2.zero;
            }
        }
    }

    private void LateUpdate()
    {
        if (!isHit) { 
            Vector3 direction = transform.eulerAngles;
            direction.x = 0f;
            direction.y = 0f;
            transform.eulerAngles = direction;
        }
    }
}
