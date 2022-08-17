using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostScript : MonoBehaviour
{
    [SerializeField]
    internal CharManagerScript managerScript;

    [SerializeField]
    internal float boostBar;

    [SerializeField]
    internal int boostLevel;

    [SerializeField]
    internal float boostLimit;

    [SerializeField]
    internal float[] lvlSpeed;

    [SerializeField]
    internal float[] lvlSize;

    private void Awake()
    {
        managerScript.horizontalScript.maxMoveSpeed = lvlSpeed[0];
        boostLimit = lvlSize[0];
    }

    void FixedUpdate()
    {
        if (boostBar >= 0f)
        {
            float boostDifference = Mathf.Abs(managerScript.rb.velocity.x) - managerScript.horizontalScript.maxMoveSpeed * 0.75f;
            boostBar = boostBar + boostDifference * Time.fixedDeltaTime;
            if (boostBar < 0f)
            {
                if (boostLevel == 0)
                {
                    boostBar = 0f;
                }
                else
                {
                    boostLevel = boostLevel - 1;
                    managerScript.horizontalScript.maxMoveSpeed = lvlSpeed[boostLevel];
                    boostLimit = lvlSize[boostLevel];
                    boostBar = boostLimit / 2;
                }
            }
        }

        if (boostBar >= boostLimit && Input.GetKeyDown(KeyCode.E))
        {
            boostLevel = boostLevel + 1;
            managerScript.horizontalScript.maxMoveSpeed = lvlSpeed[boostLevel];
            boostLimit = lvlSize[boostLevel];
            boostBar = 1f;
            managerScript.effectScript.BlueCircle(transform.position, false);
        }

        if (boostBar >= boostLimit)
        {
            boostBar = boostLimit;
        }
    }
}
