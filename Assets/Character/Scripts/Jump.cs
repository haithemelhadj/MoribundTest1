using UnityEngine;

public class Jump : MonoBehaviour
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
        JumpInput();
        FastDrop();
        FallSpeedLimit();
    }

    [Header("Jump")]
    public float jumpForce;
    public bool jumpConditions;
    public void JumpInput()
    {
        /*
        if (wallSlideScript.isWallSliding)
        {
            //jumpTimeCounter = jumpTime;
            //isWallJumping = true;
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        /**/

        if (inputsScript.jumpInput)//check jump press
        {
            /*
            //if wall jump            
            if (wallJumpingCounter > 0f)
            {
                isWallJumping = true;
                isNotMoving = true;
                wallSlideScript.isWallSliding = false;
                //jump
                Jumping(new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y));
                //inputsScript.playerRb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
                wallJumpingCounter = 0f;
                if (transform.localScale.x != wallJumpingDirection)
                {
                    movementscript.isFacingRight = !movementscript.isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1;
                    transform.localScale = localScale;
                }

                Invoke(nameof(StopWallJumping), wallJumpingDuration);
                Invoke(nameof(EnableWallMovement), canMoveAfterWallJumpDuration);
            }

            if (isWallJumping)
            {
            }
            /**/
            //if normal jump
            if (inputsScript.isGrounded || canJump) //check if the player can jump
            {
                jumpTimeCounter = jumpTime;
                isJumping = true;
            }
            if (isJumping)
            {
                if (jumpTimeCounter > 0) //check if jump duration is not over
                {
                    Jumping(new Vector2(inputsScript.playerRb.velocity.x, jumpForce));
                    jumpTimeCounter -= Time.deltaTime;
                }
                else //else stop jumping
                {
                    isJumping = false;
                }
            }

        }
    }

    //----------- wall jump
    #region WallJump
    [Header("Wall Jump")]
    public bool isWallJumping;
    public float wallJumpingDirection;
    public float wallJumpingTime = 0.2f;
    public float wallJumpingCounter;
    public float canMoveAfterWallJumpDuration = 0.2f;
    public bool isNotMoving;
    public float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(8f, 16f);

    /*
    public void WallJump()
    {
        //if is wall sliding
        if (wallSlideScript.isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (inputsScript.jumpInput && wallJumpingCounter > 0f)
        {
            //
            isWallJumping = true;
            //jump
            inputsScript.playerRb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            if (transform.localScale.x != wallJumpingDirection)
            {
                movementscript.isFacingRight = !movementscript.isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }
    /**/
    public void StopWallJumping()
    {
        isWallJumping = false;
    }

    public void EnableWallMovement()
    {
        isNotMoving = false;
    }

    #endregion

    //-------Jump

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
