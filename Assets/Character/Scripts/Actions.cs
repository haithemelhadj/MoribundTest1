using System.Collections;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public Inputs inputsScript;
    public JumpScript jumpScript;
    public WallSliding wallSlideScript;

    private void Awake()
    {
        inputsScript = GetComponent<Inputs>();
        jumpScript = GetComponent<JumpScript>();
        wallSlideScript = GetComponent<WallSliding>();
    }

    private void Update()
    {
        DashInput();
    }

    #region Dash


    [Header("Dash")]
    public float dashForce;
    public float dashTime;
    public bool canDash;
    public bool isDashing;

    public float drag;
    public void DashInput()
    {
        if (inputsScript.dashInput && canDash)
        {
            StartCoroutine(Dash());
        }
        if(wallSlideScript.isWallSliding || inputsScript.isGrounded)
        {
            StopCoroutine(Dash());
            isDashing = false;
        }
        if (!isDashing && (inputsScript.isGrounded || wallSlideScript.isWallSliding)) 
        {
            canDash = true;
        }
    }

    public IEnumerator Dash()
    {
        //set vars
        canDash = false;
        isDashing = true;
        //save gravity
        float originalGravity = inputsScript.playerRb.gravityScale;
        inputsScript.playerRb.gravityScale = 0f;
        inputsScript.playerRb.constraints.Equals(RigidbodyConstraints2D.FreezePositionY);
        //set air friction 
        float originalDrag = inputsScript.playerRb.drag;
        inputsScript.playerRb.drag = drag;
        //stop jumping
        jumpScript.isJumping = false;
        //null velocity
        inputsScript.playerRb.velocity = Vector2.zero;
        //dash 
        inputsScript.playerRb.velocity = new Vector2(transform.localScale.x * dashForce, 0f);
        yield return new WaitForSeconds(dashTime);
        //rest everything
        inputsScript.playerRb.constraints.Equals(RigidbodyConstraints2D.None);
        inputsScript.playerRb.constraints.Equals(RigidbodyConstraints2D.FreezePosition);
        inputsScript.playerRb.drag = 0f;
        inputsScript.playerRb.gravityScale = originalGravity;
        inputsScript.playerRb.drag = originalDrag;
        isDashing = false;
        yield return new WaitForSeconds(dashTime);

    }
    #endregion

    //add a jump reset like HK = make jump input with down and up and set isJumping accordengly
    //make wall jump on the same wall can go higher like HK = make player can move during jump and can jump heigher 
    //add new mechanic to wall slide is that the player can run to the wall and gain extra hight and jump distance 
    //wall jump doesnt continue jumping like a normal jump = wall jumping only adds X force and player jumps normally
}
