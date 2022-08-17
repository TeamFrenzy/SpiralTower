using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    [SerializeField]
    internal CharManagerScript managerScript;

    [SerializeField]
    internal bool grounded;

    internal Vector3 contactPoint;
    internal Vector3 impulseVector;
    internal Vector3 relativeSpeedVector;

    private void Awake()
    {
        grounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {

        /*if (collision.collider.tag == "Ground")
        {
            grounded = true;
        }

        if (collision.collider.tag == "Wall")
        {
            managerScript.wallClimbScript.wallHanging = true;
        }*/
        managerScript.wallClimbScript.wallJumping = false;
        managerScript.jumpScript.rising = false;

        contactPoint = collision.GetContact(0).point;
        impulseVector = collision.impulse.normalized;
        relativeSpeedVector = collision.relativeVelocity;
        managerScript.jumpScript.fullJump = false;

        /*
        //Crashing
        if (((Mathf.Abs(relativeSpeedVector.x) > managerScript.balanceScript.crashThreshold) || (Mathf.Abs(relativeSpeedVector.y) > managerScript.balanceScript.crashThreshold)) && impulseVector.y!=1)
        {
            managerScript.rb.velocity = Vector3.zero;
            managerScript.rb.angularVelocity = Vector3.zero;
            managerScript.rb.velocity = new Vector3(relativeSpeedVector.x, relativeSpeedVector.y+ managerScript.balanceScript.crashVerticalForceAddition, 0f) / managerScript.balanceScript.crashForceReduction;
            managerScript.balanceScript.crashstunTimer = managerScript.balanceScript.crashstunTime;
        }
        */

        //Todo choque te hace verga. Como determinar cuando Preah cae parada, o en que posicion?

        /*
        //Debug
        managerScript.debugScript.timerOn = false;
        managerScript.debugScript.secondDist = managerScript.rb.transform.position.x;
        managerScript.debugScript.lastDistBetween = Mathf.Abs(managerScript.debugScript.firstDist - managerScript.debugScript.secondDist);
        */

        /*
        if (managerScript.parryScript.parryTimer>0f)
        {
            managerScript.parryScript.parryStart = true;
        }
        else
        {
            managerScript.parryScript.parrying = false;
            managerScript.parryScript.parrySpeedMag = managerScript.parryScript.parrySpeedMagBase;

            if (managerScript.balanceScript.overBoard)
            {
                managerScript.balanceScript.crashstunTimer = relativeSpeedVector.magnitude/managerScript.balanceScript.crashStunVFactor;
            }
        }
        */

        /*
        if(managerScript.parryScript.parryDownTimer>0f)
        {
            managerScript.balanceScript.crashed = true;
        }
        */

        /*
        if (managerScript.collisionScript.grounded == true && managerScript.balanceScript.riding == true)
        {
            managerScript.balanceScript.riding = false;
            if (managerScript.inertia > managerScript.balanceScript.noControlThreshold)
            {
                managerScript.balanceScript.noControl = true;
            }
            else if (managerScript.inertia > managerScript.horizontalScript.maxMoveSpeed)
            {
                managerScript.balanceScript.imbalanced = true;
            }
            
        }
        */
    }

    /*
    private void OnCollisionStay(Collision collision)
    {
        
       /// if (collision.collider.tag == "Ground")
      //  {
         ///   grounded = true;
      //  }
        

        if (managerScript.collisionScript.grounded == true && managerScript.balanceScript.riding == true)
        {
            managerScript.balanceScript.riding = false;
            if (managerScript.inertia > managerScript.balanceScript.noControlThreshold)
            {
                managerScript.balanceScript.noControl = true;
            }
            else if (managerScript.inertia > managerScript.horizontalScript.maxMoveSpeed)
            {
                managerScript.balanceScript.imbalanced = true;
            }
        }
    }
    */




    /*
    private void OnCollisionExit(Collision collision)
    {
        
        managerScript.movementAnimationScript.animator.SetFloat("CrashForce", 0f);

        //Debug
        managerScript.debugScript.testTimer = 0f;
        managerScript.debugScript.timerOn = true;
        managerScript.debugScript.topY = 0f;
        managerScript.debugScript.firstDist = managerScript.rb.transform.position.x;
    }
    */
}
