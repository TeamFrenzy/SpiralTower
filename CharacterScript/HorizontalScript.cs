using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalScript : MonoBehaviour
{
    [SerializeField]
    internal CharManagerScript managerScript;

    [SerializeField]
    internal bool accelerating;

    [SerializeField]
    internal float accelSpeed;

    [SerializeField]
    internal float jumpAccelSpeed;

    [SerializeField]
    internal float[] accelLevel;

    [SerializeField]
    internal float maxMoveSpeed;

    [SerializeField]
    internal bool right;

    [SerializeField]
    internal bool left;

    [SerializeField]
    internal bool idle;

    [SerializeField]
    internal bool testbool1;

    [SerializeField]
    internal bool testbool2;

    [SerializeField]
    internal bool testbool3;

    void Update()
    {
        if (managerScript.xSpeed > 0f)
        {
            right = true;
            left = false;
            idle = false;
        }
        else if (managerScript.xSpeed < 0f)
        {
            left = true;
            right = false;
            idle = false;
        }
        else if (managerScript.xSpeed == 0f)
        {
            idle = true;
            right = false;
            left = false;
        }

        if (managerScript.inertia <= managerScript.boostScript.lvlSpeed[0])
        {
            accelSpeed = accelLevel[0];
        }
        else if (managerScript.inertia >= managerScript.boostScript.lvlSpeed[0])
        {
            accelSpeed = accelLevel[1];
        }
    }

    void FixedUpdate()
    {
        if(!managerScript.crouchScript.crouching && !managerScript.slidingScript.sliding && !managerScript.balanceScript.standing)
        {

        if (managerScript.collisionScript.grounded && managerScript.balanceScript.riding == false && managerScript.balanceScript.imbalanced == false && managerScript.balanceScript.crashed == false && managerScript.balanceScript.noControl == false)
        {
            testbool1 = true;
            if (Input.GetKey(KeyCode.LeftArrow) && right && managerScript.xSpeed > managerScript.boostScript.lvlSpeed[0])
            {
                //Braking
                managerScript.rb.AddForce(new Vector3(-1, 0, 0) * managerScript.brakeScript.brakingForce * Time.fixedDeltaTime);
                managerScript.brakeScript.braking = true;
                accelerating = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && right && managerScript.xSpeed <= managerScript.boostScript.lvlSpeed[0])
            {

                managerScript.rb.velocity = new Vector3(0, managerScript.rb.velocity.y, 0);
                accelerating = false;
                managerScript.brakeScript.braking = false;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                managerScript.rb.AddForce(new Vector3(-1, 0, 0) * accelSpeed * Time.fixedDeltaTime);
                accelerating = true;
                managerScript.brakeScript.braking = false;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && left && managerScript.xSpeed < -managerScript.boostScript.lvlSpeed[0])
            {
                //Braking
                managerScript.rb.AddForce(new Vector3(1, 0, 0) * managerScript.brakeScript.brakingForce * Time.fixedDeltaTime);
                managerScript.brakeScript.braking = true;
                accelerating = false;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && left && managerScript.xSpeed >= -managerScript.boostScript.lvlSpeed[0])
            {

                managerScript.rb.velocity = new Vector3(0, managerScript.rb.velocity.y, 0);
                accelerating = false;
                managerScript.brakeScript.braking = false;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                managerScript.rb.AddForce(new Vector3(1, 0, 0) * accelSpeed * Time.fixedDeltaTime);
                accelerating = true;
                managerScript.brakeScript.braking = false;
            }
            else
            {
                accelerating = false;
                managerScript.brakeScript.braking = false;
            }
        }
        else
        {
            testbool1 = false;
        }

        if (managerScript.jumpScript.jumping == true && managerScript.balanceScript.riding == false && managerScript.collisionScript.grounded == false && managerScript.jumpScript.fullJump == false)
        {
            testbool2 = true;
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                managerScript.rb.AddForce(new Vector3(-1, 0, 0) * jumpAccelSpeed * Time.fixedDeltaTime);
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                managerScript.rb.AddForce(new Vector3(1, 0, 0) * jumpAccelSpeed * Time.fixedDeltaTime);
            }
            else
            {
                if(managerScript.rb.velocity.x>0f)
                {
                    managerScript.rb.AddForce(new Vector3(-1, 0, 0) * jumpAccelSpeed/2 * Time.fixedDeltaTime);
                }
                else if(managerScript.rb.velocity.x < 0f)
                {
                    managerScript.rb.AddForce(new Vector3(1, 0, 0) * jumpAccelSpeed / 2 * Time.fixedDeltaTime);
                }
            }
        }
        else
        {
            testbool2 = false;
        }

        //Regulate XSpeed while on ground
        if(managerScript.balanceScript.riding == false && managerScript.balanceScript.imbalanced == false && managerScript.wallClimbScript.wallJumping == false && managerScript.balanceScript.noControl == false)
        {
            testbool3 = true;
            if (managerScript.rb.velocity.x > maxMoveSpeed)
            {
                managerScript.rb.velocity = new Vector3(maxMoveSpeed, managerScript.rb.velocity.y);
            }

            if (managerScript.rb.velocity.x < -maxMoveSpeed)
            {
                managerScript.rb.velocity = new Vector3(-maxMoveSpeed, managerScript.rb.velocity.y);
            }
        }
        else
        {
            testbool3 = false;
        }

        //Regulate WallJumps
        if(managerScript.wallClimbScript.wallJumping == true)
        {
            if (managerScript.rb.velocity.x > managerScript.wallClimbScript.wallJumpForce)
            {
                managerScript.rb.velocity = new Vector3(managerScript.wallClimbScript.wallJumpForce, managerScript.rb.velocity.y);
            }

            if (managerScript.rb.velocity.x < -managerScript.wallClimbScript.wallJumpForce)
            {
                managerScript.rb.velocity = new Vector3(-managerScript.wallClimbScript.wallJumpForce, managerScript.rb.velocity.y);
            }
          }
        }
    }
}

//Legacy
/*if (managerScript.collisionScript.grounded && managerScript.balanceScript.riding == false && managerScript.balanceScript.imbalanced == false && managerScript.balanceScript.crashed == false)
{
    if (Input.GetKey(KeyCode.LeftArrow))
    {
        managerScript.rb.AddForce(new Vector3(-1, 0, 0) * accelSpeed * Time.fixedDeltaTime);
        accelerating = true;
    }
    else if (Input.GetKey(KeyCode.RightArrow))
    {
        managerScript.rb.AddForce(new Vector3(1, 0, 0) * accelSpeed * Time.fixedDeltaTime);
        accelerating = true;
    }
    else
    {
        accelerating = false;
    }
*/