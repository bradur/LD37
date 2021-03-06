// Date   : 10.12.2016 12:21
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using System.Collections.Generic;

public enum AnimalType
{
    None,
    Deer
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Animal : MonoBehaviour
{

    [SerializeField]
    private AnimalType animalType;
    public AnimalType AnimalType { get { return animalType; } }

    [SerializeField]
    [Range(1,25)]
    private int health = 3;

    [SerializeField]
    [Range(0, 360)]
    private List<float> possibleDirectionsAsDegrees = new List<float>();
    private List<Vector2> possibleVectorDirections = new List<Vector2>();
    private RandomWrapper rng;

    [SerializeField]
    private List<InventoryItem> lootItems = new List<InventoryItem>();

    Rigidbody2D rb2D;
    PolygonCollider2D polygonCollider2D;

    float minDrag = 0f;
    private float stopMovementDrag = 8f;
    private float moveTimeMax = 7f;
    private float moveTimeMin = 3.5f;
    private float currentMoveTime = 0f;
    private bool moving = false;
    private bool isDead = false;
    public bool IsDead { get { return isDead; } }

    [SerializeField]
    [Range(2.5f, 10f)]
    private float speedMax = 2.5f;

    [SerializeField]
    [Range(0.5f, 2f)]
    private float speedMin = 1f;

    [SerializeField]
    private Animator animator;

    private int numProjectiles = 0;

    void Start()
    {
        animator.enabled = false;
        rb2D = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        rng = new RandomWrapper();
        foreach (float degreeDirection in possibleDirectionsAsDegrees)
        {
            float radian = degreeDirection * Mathf.Deg2Rad;
            possibleVectorDirections.Add(new Vector2(
                Mathf.Cos(radian), Mathf.Sin(radian)
            ));
        }
        MoveToRandomDirection();
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Projectile")
        {
            Debug.Log(collider2D.gameObject.name);
            collider2D.transform.SetParent(transform, true);
            bool arrowsFound = false;
            foreach (InventoryItem item in lootItems)
            {
                if (item.InventoryItemType == InventoryItemType.Arrows)
                {
                    arrowsFound = true;
                    item.Count += 1;
                    break;
                }
            }
            if (!arrowsFound)
            {
                lootItems.Add(new InventoryItem(InventoryItemType.Arrows, 1));
            }

            if (!isDead) { 
                GetHit(InventoryItemType.Bow);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        MoveToRandomDirection();
    }

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
        } else if (weapon == InventoryItemType.Bow)
        {
            healthLoss = 1;
        }
        health -= healthLoss;
        if (health < 1)
        {
            StartDying(weapon);
        }
        if (!isDead)
        {
            MoveToRandomDirection();
        }
    }

    void StartDying(InventoryItemType weapon)
    {
        animator.enabled = true;
        UIManager.main.ShowMessage(animalType, weapon, numProjectiles);
        SoundManager.main.PlaySound(SoundType.AnimalDie);
        isDead = true;
        polygonCollider2D.isTrigger = true;
        rb2D.isKinematic = true;
        rb2D.velocity = Vector2.zero;
        
        moving = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public List<InventoryItem> Loot()
    {
        animator.SetTrigger("Loot");
        return lootItems;
    }

    void MoveToRandomDirection()
    {
        rb2D.drag = minDrag;
        currentMoveTime = rng.Range(moveTimeMin, moveTimeMax);
        rb2D.velocity = rng.Choose(possibleVectorDirections) * rng.Range(speedMin, speedMax);
        moving = true;
    }

    void Update()
    {
        if (moving)
        {
            currentMoveTime -= Time.deltaTime;
            if (currentMoveTime <= 0.1f)
            {
                MoveToRandomDirection();
            }
            else if (currentMoveTime <= 2.5f)
            {
                rb2D.drag = stopMovementDrag;
            }
        }
    }
}
