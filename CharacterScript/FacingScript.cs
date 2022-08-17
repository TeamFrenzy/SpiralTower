using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingScript : MonoBehaviour
{
    [SerializeField]
    internal CharManagerScript managerScript;

    [SerializeField]
    internal bool facingRight;

    [SerializeField]
    internal bool facingLeft;

    [SerializeField]
    internal bool turningRight;

    [SerializeField]
    internal bool turningLeft;

    [SerializeField]
    internal bool turning;

    [SerializeField]
    internal float turnSpeed;

    [SerializeField]
    internal float turnTime;
    internal float turnTimer;

    [SerializeField]
    internal float test;

    private void Start()
    {
        test = transform.rotation.y;
        turnTimer = 0f;
    }

    void Update()
    {
        if(transform.rotation.eulerAngles.y==90f)
        {
            facingRight = true;
            facingLeft = false;
            turning = false;
        }
        else if(transform.rotation.eulerAngles.y==270f || transform.rotation.eulerAngles.y ==-90f)
        {
            facingLeft = true;
            facingRight = false;
            turning = false;
        }
        else
        {
            turning = true;
            facingLeft = false;
            facingRight = false;
        }

        if (facingRight && managerScript.xSpeed < 0f && turnTimer <= 0f)
        {
            turningLeft = true;
            turnTimer = turnTime;
        }
        else if (facingLeft && managerScript.xSpeed > 0f && turnTimer <= 0f)
        {
            turningRight = true;
            turnTimer = turnTime;
        }
    }

    private void FixedUpdate()
    {
        if(turnTimer>0f)
        {
            turnTimer = turnTimer - Time.fixedDeltaTime;
        }

        if(turningLeft && !facingLeft)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 270f, 0), turnSpeed*Time.fixedDeltaTime);

            if (turnTimer<=0f)
            {
                transform.rotation = Quaternion.Euler(0, 270f, 0);
                turningLeft = false;
            }
        }
        else if(turningRight && !facingRight)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 89f, 0), turnSpeed * Time.fixedDeltaTime);

            if (turnTimer <= 0f)
            {
                transform.rotation = Quaternion.Euler(0, 90f, 0);
                turningRight = false;
            }
        }
    }
}
