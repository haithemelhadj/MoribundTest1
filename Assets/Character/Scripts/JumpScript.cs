using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool jumpBool;
    public bool jumpReset;
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
        if(inputsScript.jumpInputDown)
        {
            jumpPressTime = Time.time;
            willJump = true;
        }
        // jump with get key down and up and resets jump press when player is grounded
        if (willJump && (inputsScript.isGrounded || canJump) && jumpReset)  // || wallSlideScript.isWallSliding)
        {
             jumpBool = true;
            jumpReset = false;
        }
        if(inputsScript.jumpInputUp)
        {
            jumpBool = false;
            willJump = false;
            jumpReset = true;
        }

        //long press jump get key only
        //if (inputsScript.jumpInput)//check jump press
        if(jumpBool)
        {
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
                    jumpBool = false;
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
