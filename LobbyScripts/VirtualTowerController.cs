using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualTowerController : MonoBehaviour
{
    public Vector3 rotationDirection;
    public float smoothTime;
    private float convertedTime = 10;
    private float smooth;

    void Update()
    {
        smooth = Time.deltaTime * smoothTime * convertedTime;
        transform.Rotate(rotationDirection * smooth);
    }
}
