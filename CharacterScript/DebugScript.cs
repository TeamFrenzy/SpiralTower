using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    [SerializeField]
    internal CharManagerScript managerScript;

    internal float firstDist;
    internal float secondDist;
    [SerializeField]
    internal float lastDistBetween;
    internal bool timerOn;

    [SerializeField]
    internal float testTimer;

    [SerializeField]
    internal float topY;

    [SerializeField]
    internal float gravityValue;

    // Update is called once per frame

    private void Start()
    {
        firstDist = 0f;
        secondDist = 0f;
    }
    void FixedUpdate()
    {
        if (topY < transform.position.y)
        {
            topY = transform.position.y;
        }

        if (timerOn)
        {
            testTimer = testTimer + Time.fixedDeltaTime;
        }

        //Instant full stop para simular choques
        if (Input.GetKeyDown(KeyCode.W))
        {
            managerScript.rb.velocity = Vector3.zero;
            managerScript.rb.angularVelocity = Vector3.zero;
        }
    }
}
