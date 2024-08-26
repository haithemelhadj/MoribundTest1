using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScriptv1 : MonoBehaviour
{
    public Vector2 moveDirection;
    public Rigidbody2D enemyRb;
    public float acceleration;
    public float maxWalkSpeed;
    public float maxChaseSpeed;

    public float f;


    private void Awake()
    {
        enemyRb=GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        enemyRb.velocity = Vector2.Lerp(enemyRb.velocity,direction,acceleration);
    }


    [Header("Ground Detection")]
    public Transform EdgeDetector;

    public void GroundDetection()
    {

    }
    public void EdgeDetection()
    {

    }
}
