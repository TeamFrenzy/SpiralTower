using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingScript : MonoBehaviour
{
    [SerializeField]
    internal CharManagerScript managerScript;

    [SerializeField]
    internal float slideMarginStop;

    [SerializeField]
    internal float slideStandingTime;

    [SerializeField]
    internal float slideForce;

    [SerializeField]
    internal float slideDrag;

    [SerializeField]
    internal bool sliding;

    // Update is called once per frame
    void Update()
    {
        if (managerScript.inertia > managerScript.boostScript.lvlSpeed[0] + 0.5f && managerScript.collisionScript.grounded && Input.GetKeyDown(KeyCode.DownArrow) && !sliding)
        {
            managerScript.rb.AddForce(managerScript.rb.velocity.normalized * slideForce);
            sliding = true;
            managerScript.rb.drag = slideDrag;
        }

        if (sliding && managerScript.inertia <= slideMarginStop)
        {
            if(managerScript.balanceScript.standingTimer<=0f)
            {
                managerScript.balanceScript.standingTimer = slideStandingTime;
            }
            managerScript.rb.drag = 0f;
            managerScript.rb.velocity = Vector3.zero;
            if(!managerScript.balanceScript.standing)
            {
                sliding = false;
            }
        }
    }
}
