using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MoveTowardsTarget : Action
{
    public float chaseSpeed = 0;
    public SharedTransform target;
    public SharedGameObject selfGameObject;
    public Rigidbody2D selfRb;

    public override void OnAwake()
    {
        selfRb = GetComponent<Rigidbody2D>();
    }
    public override TaskStatus OnUpdate()
    {
        if (Vector3.SqrMagnitude(transform.position - target.Value.position) < 0.1f)
        {
            return TaskStatus.Success;
        }
        selfRb.velocity = Vector3.MoveTowards(selfRb.velocity, new Vector3(transform.localScale.x * chaseSpeed, selfRb.velocity.y, 0f), chaseSpeed * 0.3f);
        if(Mathf.Sign(transform.position.x-target.Value.position.x)!=Mathf.Sign(-transform.localScale.x))
            Flip();        
        return TaskStatus.Running;
    }
    public void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x = -transform.localScale.x;
        transform.localScale = currentScale;
    }
}