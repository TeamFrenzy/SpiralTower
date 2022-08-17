using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimbScript : MonoBehaviour
{
    [SerializeField]
    internal CharManagerScript managerScript;

    [SerializeField]
    internal float wallJumpForce;

    [SerializeField]
    internal float wallJumpLength;

    [SerializeField]
    internal float wallJumpHeight;

    [SerializeField]
    internal float wallFallingSpeed;

    [SerializeField]
    internal bool wallJumping;
    internal bool wallJumpingTrigger;

    [SerializeField]
    internal bool wallHanging;

    void Update()
    {
        if(wallHanging && !managerScript.balanceScript.crashed && !managerScript.balanceScript.riding)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                wallJumpingTrigger = true;
                wallJumping = true;
            }
        }

    }

    void FixedUpdate()
    {
        if(wallJumpingTrigger)
        {
            Debug.Log(new Vector3(managerScript.collisionScript.impulseVector.x * wallJumpLength, wallJumpHeight, 0f).normalized);

            managerScript.rb.velocity = new Vector3(managerScript.collisionScript.impulseVector.x* wallJumpLength, wallJumpHeight, 0f).normalized * wallJumpForce;
            wallJumpingTrigger = false;
        }
    }
}
