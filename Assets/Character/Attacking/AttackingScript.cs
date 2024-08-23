using UnityEngine;

public class AttackingScript : MonoBehaviour
{
    public Inputs inputsScript;
    //attack variables
    public GameObject atkObj;
    public float atkRange;
    public float atkTime;
    public float atkDistance;
    public Vector2 atkPosition;
    float atkRotation;
    private void Awake()
    {
        inputsScript = GetComponent<Inputs>();
    }
    private void Update()
    {
        AttackInput();
    }

    public void AttackInput()
    {
        if (inputsScript.AttackInput)
        {
            if (inputsScript.verticalInput != 0)
            {
                //attack vertically
                atkDistance = Mathf.Sign(inputsScript.verticalInput) * (inputsScript.playerHeight / 2 + atkRange);
                atkPosition = new Vector2(transform.position.x, transform.position.y + atkDistance);
                atkRotation = 90f * Mathf.Sign(inputsScript.verticalInput) * Mathf.Sign(transform.localScale.x);//rotation is based on localScale.x
            }
            else
            {
                //attack horizontally
                atkDistance = Mathf.Sign(transform.localScale.x) * (inputsScript.playerWidth / 2 + atkRange);
                atkPosition = new Vector2(transform.position.x + atkDistance, transform.position.y);
                atkRotation = 0f;
            }
            //set position and rotation
            atkObj.transform.position = atkPosition;
            atkObj.transform.eulerAngles = new Vector3(0f, 0f, atkRotation);
            //attack and disable attack after attackTime
            atkObj.SetActive(true);
            Invoke(nameof(StopAttacking), atkTime);
        }
    }
    public void StopAttacking()
    {
        atkObj.SetActive(false);

    }
}
