using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeScript : MonoBehaviour
{
    [SerializeField]
    internal CharManagerScript managerScript;

    [SerializeField]
    internal float baseDrag;

    [SerializeField]
    internal float baseMoveTest;

    [SerializeField]
    internal bool braking;

    [SerializeField]
    internal float brakingForce;

    void FixedUpdate()
    {
        //Passive Brake
        if(managerScript.collisionScript.grounded && !managerScript.balanceScript.riding)
        {
            if(!managerScript.horizontalScript.accelerating && managerScript.inertia<=baseMoveTest && !braking)
            {
                managerScript.rb.velocity = new Vector3(0, managerScript.rb.velocity.y, 0f);
            }
            else if(!managerScript.horizontalScript.accelerating && managerScript.inertia > baseMoveTest && !managerScript.slidingScript.sliding)
            {
                managerScript.rb.drag = baseDrag;
            }
            else if(managerScript.balanceScript.imbalanced)
            {
                managerScript.rb.drag = managerScript.balanceScript.imbalanceDrag;
            }
            else if(managerScript.balanceScript.noControl)
            {
                managerScript.rb.drag = managerScript.balanceScript.noControlDrag;
            }
            else if (managerScript.slidingScript.sliding)
            {
                managerScript.rb.drag = managerScript.slidingScript.slideDrag;
            }
            else
            {
                managerScript.rb.drag = 0f;
            }
        }
    }
}
