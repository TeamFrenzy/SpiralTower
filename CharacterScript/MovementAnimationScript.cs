using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimationScript : MonoBehaviour
{
    [SerializeField]
    internal RunningScript runningScript;

    [SerializeField]
    internal Animator animator;

    [SerializeField]
    internal float tolerance;

    [SerializeField]
    internal bool tester;

    [SerializeField]
    internal float jogSpeed;

    [SerializeField]
    internal float runSpeed;

    [SerializeField]
    internal float fullRunSpeed;

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("FallSpeed", runningScript.ySpeed);
        /*
        //Rising
        if (runningScript.rising && runningScript.rb.velocity.y > tolerance)
        {
            animator.SetBool("Jogging", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Turning", false);
            animator.SetBool("Rising", true);
        }

        //Falling
        if (runningScript.falling && runningScript.rb.velocity.y < -tolerance)
        {
            animator.SetBool("Rising", false);
            animator.SetBool("Falling", true);
        }
        */
        if(runningScript.runJumping)
        {
            animator.SetBool("RunJumping", true);
        }

        //Horizontal Movement
        if (runningScript.grounded)
        {
            animator.SetBool("Grounded", true);
            animator.SetBool("RunJumping", false);
            animator.SetBool("Falling", false);
             if (runningScript.idle && runningScript.inertia < 0.05f)
                {
                    animator.SetBool("Jogging", false);
                    animator.SetBool("Turning", false);
                    animator.SetBool("Rising", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Idle", true);
                }
                else if (runningScript.inertia > 0.1f && runningScript.inertia <= (jogSpeed + 0.5))
                {
                    animator.SetBool("Idle", false);
                    animator.SetBool("Turning", false);
                    animator.SetBool("Rising", false);
                    animator.SetBool("Running", false);
                animator.SetBool("Braking", false);
                animator.SetBool("Jogging", true);
                }
                else if ((runningScript.inertia > jogSpeed + 0.5) && runningScript.inertia <= (runSpeed + 0.5))
            {
                animator.SetBool("Turning", false);
                    animator.SetBool("Rising", false);
                    animator.SetBool("Jogging", false);
                    animator.SetBool("FastRunning", false);

                animator.SetBool("Braking", false);
                animator.SetBool("Running", true);
                }
                else if (runningScript.inertia > (runSpeed + 0.5) && runningScript.inertia <= (fullRunSpeed + 0.5))
            {
                animator.SetBool("Turning", false);
                    animator.SetBool("Rising", false);
                    animator.SetBool("Jogging", false);
                    animator.SetBool("Running", false);
                animator.SetBool("Braking", false);
                animator.SetBool("FastRunning", true);
                }
        }
        else if(!runningScript.grounded)
        {
            animator.SetBool("Grounded", false);
        }
    }
}
