
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyAttackAction : Action
{

    //attack refrences
    public GameObject atkObj;
    public Animator atkAnimator;
    //attack variables
    public SharedFloat atkRange;
    public SharedFloat atkTime;
    //public float atkDistance;
    public Vector2 atkPosition;
    public float atkRotation;
    //public SharedCollider collider;
    public SharedCapsulCollider2D mobCollider;
    public SharedFloat mobHeight;
    public SharedFloat mobWidth;

    public override void OnAwake()
    {
        mobCollider = GetComponent<CapsuleCollider2D>();

    }
    public override void OnStart()
    {
        #region
        Debug.Log("attack2");
        //base.OnStart();
        mobCollider = GetComponent<CapsuleCollider2D>();
        //collider.Value = mobCollider;
        //mobWidth = mobCollider.Value.size.x;
        //mobHeight = mobCollider.size.y;
        //attack horizontally
        float atkDistance = Mathf.Sign(transform.localScale.x) * (mobWidth.Value / 2 + atkRange.Value);
        atkPosition = new Vector2(transform.position.x + atkDistance, transform.position.y);
        atkRotation = 0f;
        //set position and rotation
        atkObj.transform.position = atkPosition;
        atkObj.transform.eulerAngles = new Vector3(0f, 0f, atkRotation);
        //attack and disable attack after attackTime
        atkObj.SetActive(true);
        atkAnimator.SetBool("Attack", true);
        //MonoBehaviour.Invoke(nameof(StopAttacking), atkTime);
        cd = atkTime.Value;
        #endregion
    }

    private float cd;
    public override TaskStatus OnUpdate()
    {
        #region
        Debug.Log("attack1");
        //collider.Value = mobCollider;
        //mobWidth = mobCollider.Value.size.x;
        //mobHeight = mobCollider.size.y;
        //attack horizontally
        float atkDistance = Mathf.Sign(transform.localScale.x) * (mobWidth.Value / 2 + atkRange.Value);
        atkPosition = new Vector2(transform.position.x + atkDistance, transform.position.y);
        atkRotation = 0f;
        //set position and rotation
        atkObj.transform.position = atkPosition;
        atkObj.transform.eulerAngles = new Vector3(0f, 0f, atkRotation);
        //attack and disable attack after attackTime
        atkObj.SetActive(true);
        atkAnimator.SetBool("Attack", true);
        //MonoBehaviour.Invoke(nameof(StopAttacking), atkTime);
        cd = atkTime.Value;
        #endregion


        #region
        cd -= Time.deltaTime;
        if (cd < 0f)
        {
            cd = 0f;
            atkAnimator.SetBool("Attack", false);
            atkObj.SetActive(false);
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
        #endregion
    }


    public void StopAttacking()
    {
        atkAnimator.SetBool("Attack", false);
        atkObj.SetActive(false);

    }
    #region Attack

    #endregion


}