using System.Collections;
using UnityEngine;


public class EnemyScriptv1 : MonoBehaviour
{
    public enum _currentState { patrol, chase, attack }
    //public enum _patrolType { terretorial, wonderer }
    //public enum _attackType { melee, ranged }
    //public enum _movementType { Ground, Air }
    //public enum _detectionType { normal, trap }

    [Header("Components")]
    public Rigidbody2D mobRb;
    public Animator mobAnimator;
    public CapsuleCollider2D mobCollider;
    public float mobHeight;
    public float mobWidth;
    public LayerMask whatIsGround;

    [Header("Stats")]
    public _currentState currentState;
    //public _movementType movementType;
    public float tickSpeed;
    //public float health;
    //public float defence;
    //public float dodgeChance;


    [Header("Patrol")]
    //public _patrolType patrolType;
    public float patrolSpeed;

    [Header("Chase")]
    //public _detectionType detectionType;
    public float detectionDistance;
    public float chaseSpeed;

    [Header("attack")]
    //public _attackType attackType;
    public float attackSpeed;
    public float attackRange;
    public float attackTime;
    public float attackDmg;
    public int[] attacks;

    [Header("Extras")]
    public bool canJump;
    //public float agroRange;
    //public float Aggressiveness;
    //public float Fear;// gets lower and when it reaches 0 the mob runs from player
    //public float Intelligence;//

    private void Awake()
    {
        mobWidth = mobCollider.size.x;
        mobHeight = mobCollider.size.y;
        currentState = _currentState.patrol;
        StartCoroutine(Do());
    }
    private void Update()
    {

    }

    public IEnumerator Do()
    {
        switch (currentState)
        {
            case _currentState.patrol:
                //Patrol();
                break;
            case _currentState.chase:
                //Chase();
                break;
            case _currentState.attack:
                //Attack();
                break;
        }
        yield return new WaitForSeconds(tickSpeed);
        StartCoroutine(Do());
    }
}
