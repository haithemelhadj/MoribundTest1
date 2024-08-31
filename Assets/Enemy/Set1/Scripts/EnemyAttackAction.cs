
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyAttackAction : Action
{

    //attack refrences
    public GameObject atkObj;
    public Animator atkAnimator;
    //attack variables
    public float atkRange;
    public float atkTime;
    [HideInInspector] public float atkDistance;
    [HideInInspector] public Vector2 atkPosition;
    [HideInInspector] public float atkRotation;

    public CapsuleCollider2D mobCollider;
    public float mobHeight;
    public float mobWidth;

    public override void OnAwake()
    {
        mobCollider = GetComponent<CapsuleCollider2D>();
        mobWidth = mobCollider.size.x;
        mobHeight = mobCollider.size.y;
        //attack horizontally
        atkDistance = Mathf.Sign(transform.localScale.x) * (mobWidth / 2 + atkRange);
        atkPosition = new Vector2(transform.position.x + atkDistance, transform.position.y);
        atkRotation = 0f;
        //set position and rotation
        atkObj.transform.position = atkPosition;
        atkObj.transform.eulerAngles = new Vector3(0f, 0f, atkRotation);
        //attack and disable attack after attackTime
        atkObj.SetActive(true);
        atkAnimator.SetBool("Attack", true);
        //MonoBehaviour.Invoke(nameof(StopAttacking), atkTime);
        cd = atkTime;
    }

    private float cd;
    public override TaskStatus OnUpdate()
    {
        cd -= Time.deltaTime;
        if (cd < 0f)
        {
            cd = 0f;
            atkAnimator.SetBool("Attack", false);
            atkObj.SetActive(false);
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
    

    public void StopAttacking()
    {
        atkAnimator.SetBool("Attack", false);
        atkObj.SetActive(false);

    }
    #region Attack

    #endregion
}