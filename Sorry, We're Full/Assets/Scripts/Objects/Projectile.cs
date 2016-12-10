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

    [SerializeField]
    private bool useLifeTime = false;
    private bool launched = false;

    private bool pickable = false;

    private LayerMask launchedLayer;
    private LayerMask pickableLayer;

    public void Launch(Vector2 position, Vector2 direction, Transform parent)
    {
        launchedLayer = gameObject.layer;
        pickableLayer = LayerMask.NameToLayer("Pickable Projectile");
        Debug.Log("Launch: [" + position + "][" + direction + "]");
        transform.SetParent(parent, false);
        direction = direction - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Debug.Log(transform.eulerAngles);
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
        if (pickable)
        {
            pickable = false;
            launched = false;
            
            gameObject.SetActive(false);
            gameObject.layer = launchedLayer;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "")
        {

        }
    }

    void Update()
    {
        if (useLifeTime)
        {
            currentLifeTime -= Time.deltaTime;
            if (currentLifeTime < 0)
            {
                Destroy(gameObject);
            }
        }
        if (launched && rb2D.velocity == Vector2.zero)
        {
            launched = false;
            pickable = true;
            gameObject.layer = pickableLayer;
        }
    }

    private void LateUpdate()
    {
        Vector3 direction = transform.eulerAngles;
        direction.x = 0f;
        direction.y = 0f;
        transform.eulerAngles = direction;
    }
}