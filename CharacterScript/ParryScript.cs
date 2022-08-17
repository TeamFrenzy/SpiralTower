using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryScript : MonoBehaviour
{
    [SerializeField]
    internal CharManagerScript managerScript;

    [SerializeField]
    internal float parryTime;

    [SerializeField]
    internal float parryTimer;

    [SerializeField]
    internal float parryDownTime;

    [SerializeField]
    internal float parryDownTimer;

    [SerializeField]
    internal float parrySpeedMagBase;

    [SerializeField]
    internal float parrySpeedMagGrowth;

    [SerializeField]
    internal float parrySpeedMag;

    [SerializeField]
    internal float parryExponent;

    [SerializeField]
    internal float spinHighForce;

    [SerializeField]
    internal float animAngle;

    [SerializeField]
    internal bool parryDownTimerCD;

    [SerializeField]
    internal bool parryStart;

    [SerializeField]
    internal bool parrying;

    private void Start()
    {
        parryDownTimerCD = false;
        parrySpeedMag = parrySpeedMagBase;
    }

    void Update()
    {
        bool spinHigh = false;

        if(Input.GetKeyDown(KeyCode.A) && parryTimer<=0f && parryDownTimer<=0f)
        {
            parryTimer = parryTime;
            parryDownTimerCD = true;
        }

        if(parryTimer<=0f && parryDownTimerCD == true)
        {
            parryDownTimer = parryDownTime;
            parryDownTimerCD = false;
        }

        //Primero: Vault.
        //Segundo: Spin.
        //Tercero: Single Step (va alternando de parry a parry)
        if (parryStart)
        {
                Vector3 vForce = Vector3.zero;
                float sAngle = Vector3.SignedAngle(managerScript.collisionScript.impulseVector, managerScript.collisionScript.relativeSpeedVector, new Vector3(0, 0, 1f));
                float result = Vector3.SignedAngle(managerScript.collisionScript.impulseVector, new Vector3(1, 0, 0), new Vector3(0, 0, 1f)) + sAngle;
                if (sAngle > 0f)
                {
                    float rest = 90f - sAngle;
                    float restResult = rest / parryExponent;
                    float finalResult = result + restResult;
                    vForce = Quaternion.AngleAxis(-finalResult, Vector3.right) * Vector3.up;
                }
                else if (sAngle < 0f)
                {
                    float rest = 90f + sAngle;
                    float restResult = rest / parryExponent;
                    float finalResult = result - restResult;
                    vForce = Quaternion.AngleAxis(-finalResult, Vector3.right) * Vector3.up;
                }
                else //Spinning High
                {
                    spinHigh = true;
                }
                managerScript.rb.velocity = Vector3.zero;
                managerScript.rb.angularVelocity = Vector3.zero;
                if (spinHigh == false)
                {
                    managerScript.rb.velocity = (new Vector3(vForce.y, vForce.z).normalized * managerScript.collisionScript.relativeSpeedVector.magnitude * parrySpeedMag) + new Vector3(0f, 0.2f, 0f);
                animAngle = -Vector3.SignedAngle(managerScript.rb.velocity, new Vector3(1,0,0), new Vector3(0,0,1));
                if(animAngle >90f)
                {
                    animAngle = animAngle - (2*(animAngle-90f));
                }
                managerScript.movementAnimationScript.animator.SetFloat("ParryAngle", animAngle);
                }
                else if (spinHigh == true)
                {
                    managerScript.rb.velocity = (new Vector3(0f, 1f, 0f).normalized * managerScript.collisionScript.relativeSpeedVector.magnitude * spinHighForce) + new Vector3(0f, 0.2f, 0f);
                managerScript.movementAnimationScript.animator.SetFloat("ParryAngle", 90);
            }
            managerScript.movementAnimationScript.animator.SetTrigger("ParryTrigger");
            parryStart = false;
            parrying = true;
            parryDownTimerCD = false;
            parryTimer = 0f;
            parrySpeedMag = parrySpeedMag + parrySpeedMagGrowth;

            managerScript.balanceScript.riding = true;

                managerScript.effectScript.BlueCircle(managerScript.collisionScript.contactPoint, true);
            
        }
    }

    private void FixedUpdate()
    {
        if(parryTimer>0f)
        {
            parryTimer = parryTimer - Time.fixedDeltaTime;
        }

        if(parryDownTimer>0f)
        {
            parryDownTimer = parryDownTimer - Time.fixedDeltaTime;
        }
    }
}


//Legacy
/*[SerializeField]
    internal ManagerScript managerScript;

    [SerializeField]
    internal float parryTimer;

    [SerializeField]
    internal float parryTime;

    [SerializeField]
    internal float parrySpeedMag;

    [SerializeField]
    internal float spinHighForce;

    void Update()
    {
        bool spinHigh = false;

        //Primero: Vault.
        //Segundo: Spin.
        //Tercero: Single Step (va alternando de parry a parry)
        if (parryTimer > 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                managerScript.collisionScript.grounded = false;
                Vector3 vForce = Vector3.zero;
                float sAngle = Vector3.SignedAngle(managerScript.collisionScript.impulseVector, managerScript.collisionScript.relativeSpeedVector, new Vector3(0, 0, 1f));
                float result = Vector3.SignedAngle(managerScript.collisionScript.impulseVector, new Vector3(1, 0, 0), new Vector3(0, 0, 1f)) + sAngle;
                if (sAngle > 0f)
                {
                    float rest = 90f - sAngle;
                    float restResult = rest / 5f;
                    float finalResult = result + restResult;
                    vForce = Quaternion.AngleAxis(-finalResult, Vector3.right) * Vector3.up;
                }
                else if (sAngle < 0f)
                {
                    float rest = 90f + sAngle;
                    float restResult = rest / 5f;
                    float finalResult = result - restResult;
                    vForce = Quaternion.AngleAxis(-finalResult, Vector3.right) * Vector3.up;
                }
                else //Spinning High
                {
                    spinHigh = true;
                }
                managerScript.rb.velocity = Vector3.zero;
                managerScript.rb.angularVelocity = Vector3.zero;
                if (spinHigh == false)
                {
                    managerScript.rb.velocity = (new Vector3(vForce.y, vForce.z).normalized * managerScript.collisionScript.relativeSpeedVector.magnitude * parrySpeedMag) + new Vector3(0f, 0.2f, 0f);
                }
                else if (spinHigh == true)
                {
                    managerScript.rb.velocity = (new Vector3(0f, 1f, 0f).normalized * managerScript.collisionScript.relativeSpeedVector.magnitude * spinHighForce) + new Vector3(0f, 0.2f, 0f);
                }

                managerScript.balanceScript.riding = true;

                managerScript.effectScript.BlueCircle(managerScript.collisionScript.contactPoint);
            } 
        }
    }

    private void FixedUpdate()
    {
        if(parryTimer>0f)
        {
            parryTimer = parryTimer - Time.fixedDeltaTime;
        }
    }

    public void AssignParryTime()
    {
        parryTimer = parryTime;
    }

    public void CancelParryTime()
    {
        parryTimer = 0f;
    }
}
*/