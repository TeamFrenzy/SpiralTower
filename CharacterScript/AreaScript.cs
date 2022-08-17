using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScript : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "MC")
        {
            if (collision.collider.GetComponent<CharManagerScript>().parryScript.parryTimer <= 0f)
            {
                collision.collider.GetComponent<CollisionScript>().grounded = true;
            }
        }
    }
    

    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "MC")
        {
            if (collision.collider.GetComponent<CharManagerScript>().parryScript.parryTimer <= 0f)
            {
                collision.collider.GetComponent<CollisionScript>().grounded = true;
            }
        }
    }
    

    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "MC")
        {
            collision.collider.GetComponent<CollisionScript>().grounded = false;
        }
    }
    
}
