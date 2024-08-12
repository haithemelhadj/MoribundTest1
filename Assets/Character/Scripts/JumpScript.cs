using UnityEngine;

public class JumpScript : MonoBehaviour
{
    [Header("Refrences")]
    public Inputs inputsScript;
    public WallSliding wallSlideScript;
    public Actions ActionsScript;
    public Movement movementscript;

    private void Awake()
    {
        //get scripts
        inputsScript = GetComponent<Inputs>();
        ActionsScript = GetComponent<Actions>();
        wallSlideScript = GetComponent<WallSliding>();
        movementscript = GetComponent<Movement>();
    }

    private void Update()
    {
        if (ActionsScript.isDashing) return; //if dashing stop movement
        VariableJump();
        CyoteTime();
        JumpBuffer();
        JumpInput();
        FastDrop();
        FallSpeedLimit();
    }
    [Header("Jump")]
    public float jumpForce;
    public bool jumpConditions;
    public bool jumpInputConfirmed;
    public bool jumpReset;
    [Header("wall Jump")]
    public float wallJumpDuration;
    public float wallJumpPressTime;
    public bool isWallJumping;
    public Vector2 jumpDirection;
    public Vector2 wallJumpDirection;
    public void JumpInput()
    {
        if (inputsScript.jumpInputDown)
        {
            jumpPressTime = Time.time;
            willJump = true;
            //put jump direction based on player state
            if (wallSlideScript.isWallSliding)
            {
                wallJumpPressTime = Time.time;
            }
        }
        // jump with get key down and up and resets jump press when player is grounded
        if (willJump && jumpReset && (inputsScript.isGrounded || canJump || wallSlideScript.isWallSliding))
        {
            jumpInputConfirmed = true;
            jumpReset = false;
        }
        if (inputsScript.jumpInputUp)
        {
            jumpInputConfirmed = false;
            willJump = false;
            jumpReset = true;
        }


        if (jumpInputConfirmed)
        {
            //check if the player can jump
            if (inputsScript.isGrounded || canJump|| wallSlideScript.isWallSliding)
            {
                jumpTimeCounter = jumpTime;
                isJumping = true;
                //set jumping animation
                inputsScript.playerAnimator.SetBool("isJumping", isJumping);
            }

            if (isJumping)
            {
                //check if walljumping duration is not over
                if (isWallJumping || (Time.time - wallJumpPressTime < wallJumpDuration && wallSlideScript.isWallSliding)) 
                {
                    isWallJumping = true;
                    jumpDirection = new Vector2(-transform.localScale.x * wallJumpDirection.x, wallJumpDirection.y);
                }
                else
                {
                    isWallJumping = false;
                    jumpDirection = new Vector2(inputsScript.playerRb.velocity.x, jumpForce);
                }
                //check if jump duration is not over
                if (jumpTimeCounter > 0)
                {
                    Jumping(jumpDirection);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else //else stop jumping
                {
                    isWallJumping = false;
                    isJumping = false;
                    //set jumping animation
                    inputsScript.playerAnimator.SetBool("isJumping", isJumping);
                    jumpInputConfirmed = false;
                }                
            }
        }
    }



    public void Jumping(Vector2 JumpDirection)
    {
        inputsScript.playerRb.velocity = JumpDirection;
        canJump = false;
    }

    [Header("Variable Jump")]
    public float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;
    public void VariableJump()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            //set jumping animation
            inputsScript.playerAnimator.SetBool("isJumping", isJumping);
            isWallJumping = false;
        }
    }

    //-----------Jump Buffer
    [Header("Jump Buffer")]
    public float jumpPressTime;
    public float jumpBufferTime;
    public bool willJump;
    public void JumpBuffer()
    {
        if (Time.time - jumpPressTime > jumpBufferTime)
        {
            willJump = false;
        }

    }

    //-----------cyote time
    [Header("Cyote Time")]
    public float LastGrounded;
    public float cyoteTime;
    public bool canJump;
    public void CyoteTime()
    {
        if (Time.time - LastGrounded > cyoteTime)
        {
            canJump = false;
        }
    }

    //limit fall speed 
    [Header("Fall Controll")]
    public float maxFallSpeed;
    public void FallSpeedLimit()
    {
        if (inputsScript.playerRb.velocity.y < -maxFallSpeed)
        {
            inputsScript.playerRb.velocity = new Vector2(inputsScript.playerRb.velocity.x, -maxFallSpeed);
        }
    }

    //when player is falling make him fall faster
    public void FastDrop()
    {
        if (inputsScript.playerRb.velocity.y < 2f)
        {
            inputsScript.playerRb.velocity += Vector2.up * Physics2D.gravity.y * 5 * Time.deltaTime;
        }
    }
}
