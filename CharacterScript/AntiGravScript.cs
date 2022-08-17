using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravScript : MonoBehaviour
{
    [SerializeField]
    internal CharManagerScript managerScript;

    [SerializeField]
    internal bool antiGrav;

    [SerializeField]
    internal float gravValue;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (antiGrav == true)
            {
                antiGrav = false;
            }
            else if (antiGrav == false)
            {
                antiGrav = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (antiGrav)
        {
            managerScript.jumpScript.jumpSpeed = managerScript.inertia;

            if (managerScript.jumpScript.jumpSpeed < managerScript.jumpScript.baseJumpSpeed)
            {
                managerScript.jumpScript.jumpSpeed = managerScript.jumpScript.baseJumpSpeed;
            }
            float dif = managerScript.jumpScript.baseJumpSpeed / managerScript.jumpScript.jumpSpeed;
            managerScript.jumpScript.jumpLimit = managerScript.jumpScript.maxJumpTime * dif;
            float firstValue = managerScript.inertia / managerScript.jumpScript.baseJumpSpeed;
            gravValue = firstValue * firstValue;

            //Debug
            managerScript.debugScript.gravityValue = gravValue;

            if (gravValue >= 1f)
            {
                managerScript.rb.AddForce(Physics.gravity * managerScript.rb.mass * (gravValue - 1));
            }
        }
        else if (!antiGrav)
        {
            if (managerScript.jumpScript.hopping)
            {
                managerScript.jumpScript.jumpLimit = managerScript.jumpScript.hopTime;
            }
            else
            {
                managerScript.jumpScript.jumpLimit = managerScript.jumpScript.maxJumpTime;
            }
            managerScript.jumpScript.jumpSpeed = managerScript.inertia / 2f;
        }
    }
}
