using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class PassivePatrol : Action
{
    public LayerMask whatIsGround;
    public Rigidbody2D selfRb;
    public CapsuleCollider2D mobCollider;

    public float mobHeight;
    public float mobWidth;

    public float patrolSpeed;

    public float extraGroundCheckDistance;
    public float scanRange = 1f;

    public override void OnAwake()
    {
        selfRb = GetComponent<Rigidbody2D>();
        mobCollider = GetComponent<CapsuleCollider2D>();
        mobWidth = mobCollider.size.x;
        mobHeight = mobCollider.size.y;
    }
    public override TaskStatus OnUpdate()
    {

        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position + (Vector3)mobCollider.offset + new Vector3((mobWidth / 2 + scanRange) * Mathf.Sign(transform.localScale.x), 0, 0), Vector2.right * Mathf.Sign(transform.localScale.x), extraGroundCheckDistance, whatIsGround);
        Debug.DrawRay(transform.position + (Vector3)mobCollider.offset + new Vector3((mobWidth / 2 + scanRange) * Mathf.Sign(transform.localScale.x), 0, 0), Vector2.right * Mathf.Sign(transform.localScale.x) * extraGroundCheckDistance, color: Color.green);
        RaycastHit2D ldgeCheck = Physics2D.Raycast(transform.position + (Vector3)mobCollider.offset + new Vector3((mobWidth / 2 + scanRange) * Mathf.Sign(transform.localScale.x), 0, 0), Vector2.down, mobHeight / 2 + extraGroundCheckDistance, whatIsGround);
        Debug.DrawRay(transform.position + (Vector3)mobCollider.offset + new Vector3((mobWidth / 2 + scanRange) * Mathf.Sign(transform.localScale.x), 0, 0), Vector2.down * (mobHeight / 2 + extraGroundCheckDistance), color: Color.red);


        if (!ldgeCheck || wallCheck)
        {

            Flip();
        }

        selfRb.velocity = Vector3.MoveTowards(selfRb.velocity, new Vector3(transform.localScale.x * patrolSpeed, selfRb.velocity.y, 0f), patrolSpeed * 0.3f);
        return TaskStatus.Running;
    }


    public void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x = -transform.localScale.x;
        transform.localScale = currentScale;
    }

}