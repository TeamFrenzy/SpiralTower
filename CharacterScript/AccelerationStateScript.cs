using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationStateScript : MonoBehaviour
{

    [SerializeField]
    internal CharManagerScript managerScript;

    [SerializeField]
    internal bool idleX;

    [SerializeField]
    internal bool acceleratingX;

    [SerializeField]
    internal bool deacceleratingX;

    [SerializeField]
    internal float lastVelocityX;

    [SerializeField]
    internal bool idleY;

    [SerializeField]
    internal bool acceleratingY;

    [SerializeField]
    internal bool deacceleratingY;

    [SerializeField]
    internal float lastVelocityY;

    private void Awake()
    {
        lastVelocityX = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //X
        if (managerScript.inertia < 0.01f)
        {
            idleX = true;
            acceleratingX = false;
            deacceleratingX = false;
        }
        else if ((managerScript.inertia > lastVelocityX) || (managerScript.inertia == lastVelocityX))
        {
            acceleratingX = true;
            idleX = false;
            deacceleratingX = false;
        }
        else
        {
            deacceleratingX = true;
            idleX = false;
            acceleratingX = false;
        }
        lastVelocityX = managerScript.inertia;

        //Y
        if (managerScript.vInertia < 0.01f)
        {
            idleY = true;
            acceleratingY = false;
            deacceleratingY = false;
        }
        else if ((managerScript.vInertia > lastVelocityY) || (managerScript.vInertia == lastVelocityY))
        {
            acceleratingY = true;
            idleY = false;
            deacceleratingY = false;
        }
        else
        {
            deacceleratingY = true;
            idleY = false;
            acceleratingY = false;
        }
        lastVelocityY = managerScript.vInertia;
    }
}

//Legacy

/*
 * if (managerScript.rb.velocity.magnitude < 0.01f)
        {
            idle = true;
            accelerating = false;
            deaccelerating = false;
        }
        else if ((managerScript.rb.velocity.magnitude > lastVelocity.magnitude) || (managerScript.rb.velocity.magnitude == lastVelocity.magnitude))
        {
            accelerating = true;
            idle = false;
            deaccelerating = false;
        }
        else
        {
            deaccelerating = true;
            idle = false;
            accelerating = false;
        }
        lastVelocity = managerScript.rb.velocity;
*/