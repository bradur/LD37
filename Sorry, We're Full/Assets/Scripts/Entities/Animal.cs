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
    [Range(1, 5)]
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
            collider2D.transform.SetParent(transform, true);
            GetHit(Weapon.Bow);
        }
    }

    void GetHit(Weapon weapon)
    {
        health -= 1;
        if (health < 1)
        {
            StartDying(weapon);
        }
    }

    void StartDying(Weapon weapon)
    {
        UIManager.main.ShowMessage(animalType, weapon, numProjectiles);
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
                rb2D.drag = minDrag;
                MoveToRandomDirection();
            }
            else if (currentMoveTime <= 2.5f)
            {
                rb2D.drag = stopMovementDrag;
            }
        }
    }
}
