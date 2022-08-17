using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSpaceMessageScript : MonoBehaviour
{
    public float speed;
    public float minHeight = -345f;
    public float maxHeight = -300f;
    public bool up = true;

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<RectTransform>().position.y <= minHeight && !up)
        {
            Debug.Log("TriggerUp");
            up = true;
        }
        else if(GetComponent<RectTransform>().position.y >= maxHeight && up)
        {
            Debug.Log("TriggerDown");
            up = false;
        }

        if(up)
        {
            GetComponent<RectTransform>().position = new Vector3(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y + Time.deltaTime * speed, 0f);
        }
        else if(!up)
        {
            GetComponent<RectTransform>().position = new Vector3(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y - Time.deltaTime * speed, 0f);
        }
    }
}
