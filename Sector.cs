using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sector
{
    public GameObject sectorObject;
    public int id;
    public Vector3 location;
    public float showCasingAngle;
    public float showCasingHeight;
    public string type;
    public float zoomSize;
    public Room[] sectorRooms;
}
