using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchScript : MonoBehaviour
{
    [SerializeField]
    internal CharManagerScript managerScript;

    [SerializeField]
    internal bool crouching;

    // Update is called once per frame
    void Update()
    {
        if(managerScript.inertia<= managerScript.boostScript.lvlSpeed[0] && managerScript.collisionScript.grounded)
        {
            if(Input.GetKey(KeyCode.DownArrow))
            {
                crouching = true;
            }
            else
            {
                crouching = false;
            }
        }
        else
        {
            crouching = false;
        }
    }
}
