using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonSpin : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, -0.12f));
    }
}
