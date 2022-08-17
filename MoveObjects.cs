using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    public string draggingTag;

    public Camera cam;

    public Vector3 dis;

    public int roomNumber;

    public void FixedUpdate()
    {
        if(Input.touchCount!= 1)
        {
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if(touch.phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(pos);

            Debug.Log("Touching!");

            if(Physics.Raycast(ray, out hit) && hit.collider.tag == draggingTag)
            {
                roomNumber = int.Parse(hit.transform.gameObject.name);
            }

        }
    }
}
