using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScriptNew : MonoBehaviour
{ 
    [SerializeField]
    internal bool grounded;
    [SerializeField]
    internal bool rising;
    [SerializeField]
    internal bool fullJump;

    internal Vector3 contactPoint;
    internal Vector3 impulseVector;
    internal Vector3 relativeSpeedVector;

    private void Awake()
    {
        grounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rising = false;

        contactPoint = collision.GetContact(0).point;
        impulseVector = collision.impulse.normalized;
        relativeSpeedVector = collision.relativeVelocity;
        fullJump = false;
    }
}
