using UnityEngine;

public class WallSliding : MonoBehaviour
{
    public Inputs inputsScript;
    public JumpScript jumpingScript;

    private void Awake()
    {
        //get scripts
        inputsScript = GetComponent<Inputs>();
        jumpingScript = GetComponent<JumpScript>();
    }

    private void Update()
    {
        //WallSlide();
        LedgeBump();
    }

    #region WallDetection

    [Header("Ledge Detection")]
    public LayerMask whatIsWall;
    public bool WallDetectionUpper()
    {
        return Physics2D.Raycast(transform.position + new Vector3(0, inputsScript.playerHeight / 2, 0), new Vector2(transform.localScale.x, 0f), inputsScript.playerWidth / 2 + inputsScript.extraGroundCheckDistance, whatIsWall);
    }

    public bool WallDetectionMiddle()
    {
        return Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0f), inputsScript.playerWidth / 2 + inputsScript.extraGroundCheckDistance, whatIsWall);
    }
    public bool WallDetectionLower()
    {
        return Physics2D.Raycast(transform.position - new Vector3(0, inputsScript.playerHeight / 2, 0), new Vector2(transform.localScale.x, 0f), inputsScript.playerWidth / 2 + inputsScript.extraGroundCheckDistance, whatIsWall);
    }

    #endregion 

    #region Wall Slide



    /*
    [Header("Wall Slide")]
    public bool isWallSliding;
    public float wallSlidingSpeed;
    public void WallSlide()
    {
        if (inputsScript.isGrounded || inputsScript.playerRb.velocity.y > 0)
        {
            isWallSliding = false;
            //wall jump values set
            jumpingScript.wallJumpingCounter -= Time.deltaTime;
            return;
        }
        if (!jumpingScript.isWallJumping)
        {
            if (WallDetectionUpper() || WallDetectionMiddle() || WallDetectionLower())
            {
                isWallSliding = true;
                inputsScript.playerRb.velocity = new Vector2(inputsScript.playerRb.velocity.x, -wallSlidingSpeed);

                //wall jump values set
                jumpingScript.isWallJumping = false;
                jumpingScript.wallJumpingDirection = -transform.localScale.x;
                jumpingScript.wallJumpingCounter = jumpingScript.wallJumpingTime;

                CancelInvoke(nameof(jumpingScript.StopWallJumping));
                CancelInvoke(nameof(jumpingScript.EnableWallMovement));
            }
            else
            {
                isWallSliding = false;
            }
        }
    }
    /**/
    #endregion

    #region Ledge Bump
    [Header("Ledge Bump")]
    public float bumpForce;
    public bool isLedgeBumping;
    public float bumpTime;
    public void LedgeBump()
    {
        if (WallDetectionLower() && !WallDetectionMiddle() && inputsScript.playerRb.velocity.y > 0)
        {
            isLedgeBumping = true;
            inputsScript.playerRb.velocity = new Vector2(0f, inputsScript.playerRb.velocity.y);
            Invoke("CancleLedgeBumb", bumpTime);
        }
    }

    private void CancleLedgeBumb()
    {
        isLedgeBumping = false;
    }

    #endregion
}
