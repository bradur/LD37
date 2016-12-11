// Date   : 11.12.2016 08:57
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class RandomMovement : MonoBehaviour {
    Rigidbody2D rb2D;

    [SerializeField]
    [Range(0, 360)]
    private List<float> possibleDirectionsAsDegrees = new List<float>();
    private List<Vector2> possibleVectorDirections = new List<Vector2>();

    float minDrag = 0f;
    private float stopMovementDrag = 8f;

    [SerializeField]
    [Range(2.5f, 10f)]
    private float moveTimeMax = 7f;

    [SerializeField]
    [Range(0.5f, 3.5f)]
    private float moveTimeMin = 3.5f;

    [SerializeField]
    [Range(0.25f, 2.5f)]
    private float startDrag = 2.5f;
    private float currentMoveTime = 0f;
    private bool moving = false;
    private bool isDead = false;
    private RandomWrapper rng;

    [SerializeField]
    [Range(2.5f, 10f)]
    private float speedMax = 2.5f;

    [SerializeField]
    [Range(0.5f, 2f)]
    private float speedMin = 1f;

    void Start () {
        rb2D = GetComponent<Rigidbody2D>();
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
