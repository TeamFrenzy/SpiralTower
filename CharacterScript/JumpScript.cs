using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    [SerializeField]
    internal CharManagerScript managerScript;

    [SerializeField]
    internal float baseJumpSpeed;

    [SerializeField]
    internal float jumpSpeed;

    [SerializeField]
    internal float jumpTimer;

    [SerializeField]
    internal float maxJumpTime;

    [SerializeField]
    internal float jumpLimit;

    [SerializeField]
    internal float hopTime;

    [SerializeField]
    internal bool jumping;

    [SerializeField]
    internal bool rising;

    [SerializeField]
    internal bool falling;

    [SerializeField]
    internal bool hopping;

    [SerializeField]
    internal bool fullJump;

    [SerializeField]
    internal float fullJumpForce;

    void Update()
    {
        if (managerScript.balanceScript.imbalanced == false && managerScript.balanceScript.crashed == false && managerScript.balanceScript.riding == false && managerScript.wallClimbScript.wallHanging == false && managerScript.balanceScript.crashed == false && managerScript.balanceScript.standing == false && managerScript.crouchScript.crouching == false && managerScript.slidingScript.sliding == false)
        {
        if (Input.GetKeyDown(KeyCode.Space) && managerScript.collisionScript.grounded)
        {
            jumpTimer = 0f;
            jumpLimit = maxJumpTime;
            jumping = true;
            rising = true;
        }

        if (Input.GetKey(KeyCode.Space) && rising)
        {
            rising = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && jumpTimer < hopTime && rising)
        {
            jumpLimit = hopTime;
            hopping = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && rising && !hopping)
        {
            jumpTimer = maxJumpTime;
            managerScript.rb.velocity = new Vector3(managerScript.rb.velocity.x, 0, 0);
            rising = false;
        }

        }
        //Full Jump Script
        /*
        else if(managerScript.balanceScript.imbalanced == true && Input.GetKeyDown(KeyCode.Space) && managerScript.collisionScript.grounded && managerScript.balanceScript.crashed == false && managerScript.balanceScript.crashed == false && managerScript.balanceScript.standing == false)
        {
            float dir = 0f;
            {
                if (managerScript.rb.velocity.x > 0f) 
                {
                    dir = 1f;
                }
                else if (managerScript.rb.velocity.x < 0f)
                {
                    dir = -1f;
                }
            }
            managerScript.rb.AddForce(new Vector3(dir, 1, 0).normalized * fullJumpForce, ForceMode.Impulse);
            fullJump = true;
        }
        */
    }

    //Los Hop sirven para conservar la inercia mientras se esta en desbalance, para estirarla un poco mas hasta acercarse todo lo posible al filo del abismo para pegar el mega salto.

    void FixedUpdate()
    {
        if(!managerScript.crouchScript.crouching && !managerScript.slidingScript.sliding)
        {
        if (jumpSpeed < baseJumpSpeed)
        {
            jumpSpeed = baseJumpSpeed;
        }

        if (jumpTimer < jumpLimit)
        {
            jumpTimer = jumpTimer + Time.fixedDeltaTime;
        }

        if (rising)
        {
            managerScript.rb.velocity = new Vector3(managerScript.rb.velocity.x, jumpSpeed, 0);
        }

        if (jumpTimer >= jumpLimit && rising)
        {
            managerScript.rb.velocity = new Vector3(managerScript.rb.velocity.x, 0, 0);
            jumpTimer = maxJumpTime;
            rising = false;
            hopping = false;
        }

        if (managerScript.rb.velocity.y < -0.1f)
        {
            falling = true;
        }
        else
        {
            falling = false;
        }
    }
    }
}
