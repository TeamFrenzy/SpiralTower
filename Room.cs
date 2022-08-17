using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room
{
    public GameObject roomObject;
    public int id;
    public Vector3 location;
    public float showCasingAngle;
    public float showCasingHeight;
    public Transition[] transitions;
    public string type;
}
