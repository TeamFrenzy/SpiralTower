using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceScript : MonoBehaviour
{
    [SerializeField]
    internal CharManagerScript managerScript;

    [SerializeField]
    internal bool riding;

    [SerializeField]
    internal bool imbalanced;

    [SerializeField]
    internal float imbalanceDrag;

    [SerializeField]
    internal bool recovering;

    [SerializeField]
    internal bool crashed;

    [SerializeField]
    internal float crashThreshold;

    [SerializeField]
    internal float crashVThreshold;

    [SerializeField]
    internal float crashstunTime;

    [SerializeField]
    internal float crashstunTimeV;

    [SerializeField]
    internal float crashstunTimer;

    [SerializeField]
    internal float crashForceReduction;

    [SerializeField]
    internal float crashVerticalForceAddition;

    [SerializeField]
    internal bool standing;

    [SerializeField]
    internal float standingTime;

    [SerializeField]
    internal float standingTimer;

    [SerializeField]
    internal bool crashingCD;

    [SerializeField]
    internal float overBoardThreshold;

    [SerializeField]
    internal float crashStunVFactor;

    [SerializeField]
    internal bool overBoard;

    [SerializeField]
    internal bool noControl;

    [SerializeField]
    internal float noControlThreshold;

    [SerializeField]
    internal float noControlDrag;

    void Awake()
    {
        riding = false;
        imbalanced = false;
        recovering = false;
        noControl = false;
        crashed = false;
        crashingCD = false;
    }
    void FixedUpdate()
    {
        if(managerScript.ySpeed<= -overBoardThreshold)
        {
            overBoard = true;
        }

        if(imbalanced)
        {
            managerScript.rb.drag = imbalanceDrag;
            if(managerScript.inertia<= managerScript.horizontalScript.maxMoveSpeed)
            {
                imbalanced = false;
                managerScript.rb.drag = 0f;
            }
        }

        if (noControl)
        {
            managerScript.rb.drag = noControlDrag;
            if (managerScript.inertia <= managerScript.horizontalScript.maxMoveSpeed)
            {
                crashstunTimer = crashstunTime;
                noControl = false;
                managerScript.rb.drag = 0f;
            }
        }

        if (crashstunTimer>0f && crashingCD == false)
        {
            crashed = true;
            crashstunTimer = crashstunTimer - Time.fixedDeltaTime;
        }

        if (crashed)
        {
            if (crashstunTimer <= 0f)
            {    
                crashed = false;
                crashingCD = true;
                standingTimer = standingTime;
            }
        }

        if (standingTimer > 0f && (crashingCD == true || !managerScript.slidingScript.sliding))
        {
            standing = true;
            standingTimer = standingTimer - Time.fixedDeltaTime;
        }

        if (standing)
        {
            if (standingTimer <= 0f)
            {
                standing = false;
                crashingCD = false;
                overBoard = false;
            }
        }
    }
}
