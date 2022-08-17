using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEventScript : MonoBehaviour
{
    public delegate void EventHandler();
    public event EventHandler CollideWithPlayer;


    // Checking a reference to a collider is better than using strings.
    [SerializeField] Collider playerCollider;


    void OnTriggerEnter(Collider collider)
    {
        if (collider == playerCollider) CollideWithPlayer();
    }

}
